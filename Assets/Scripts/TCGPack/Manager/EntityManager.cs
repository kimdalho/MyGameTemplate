using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Card_GamePackage
{
    public class EntityManager : Singleton<EntityManager>
    {
        [SerializeField] GameObject entityPrefab;
        [SerializeField] GameObject damagePrefab;
        [SerializeField] List<Entity> myEntities = new List<Entity>();
        [SerializeField] List<Entity> otherEntities = new List<Entity>();
        [SerializeField] GameObject entityPicker;
        [SerializeField] Entity myBossEntity;
        [SerializeField] Entity myEmptyEntity;
        [SerializeField] Entity OtherBossEntity;

        public const int MAX_ENTITY_COUNT = 6;

        //조건 : 현제 나의 Entity가 Full  = MaxCout보다 많다 그리고 MyEmptyEntity가 존재하지않는다.
        public bool IsFullMyEntities => myEntities.Count >= MAX_ENTITY_COUNT && !ExistMyEmptyEntity;
        //조건 : 현제 Other의 Entity가 Full  = MaxCout보다 많다
        bool IsFullOtherEntites => otherEntities.Count >= MAX_ENTITY_COUNT;

        bool ExistTargetPickEntity => targetPickEntity != null;
        //조건 :  MyEmptyEntity의 존재여부  = 나의 Entities의 존재여부
        bool ExistMyEmptyEntity => myEntities.Exists(x => x == myEmptyEntity);
        //조건 :  MyEmptityIndex의 정의  = 나의 Entities의 존재여부
        int MyEmptityIndex => myEntities.FindIndex(x => x == myEmptyEntity);
        bool CanMosueInput => TurnManager.Instance.myTurn && !TurnManager.Instance.isLoading;

        Entity selectEntity;
        Entity targetPickEntity;
        //캐싱
        WaitForSeconds delay1 = new WaitForSeconds(1);
        WaitForSeconds delay2 = new WaitForSeconds(2);

        private void Start()
        {
            TurnManager.OnTurnStarted += OnTurnStarted;
        }

        private void OnDestroy()
        {
            TurnManager.OnTurnStarted -= OnTurnStarted;
        }

        private void OnTurnStarted(bool myTurn)
        {
            AttackableReset(myTurn);

            if (!myTurn)
                StartCoroutine(AICo());
        }

        private void Update()
        {
            ShowTargetPicker(ExistTargetPickEntity);
        }

        void ShowTargetPicker(bool isShow)
        {
            entityPicker.SetActive(isShow);

            if (isShow)
                entityPicker.transform.position = targetPickEntity.transform.position;
        }

        IEnumerator AICo()
        {
            //1. 카드를 Put
            CardManager.Instance.TryPutCard(false);
            yield return delay1;


            //attacker를 담을 버퍼 attacker가 필수적으로 필요
            //otehrEntites를 바로 사용하면 삭제되는 요소와 for가 충돌을 일으킨다
            //otherEntites를 사용하면 순차적의 공격만이 가능해진다 셔플을 해버리면 순서가 마음대로 바뀌기때문

            List<Entity> attackers = new List<Entity>(otherEntities.FindAll(x => x.attackable == true));

            //리스트순차 공격이 아닌 랜덤한 공격구현을위한 attackers의 셔플이다
            for (int i = 0; i < attackers.Count; i++)
            {
                //무작위 attacker index의 추출
                int rand = Random.Range(i, attackers.Count);
                //버퍼리스트에 스왑
                Entity temp = attackers[i];
                attackers[i] = attackers[rand];
                attackers[rand] = temp;
            }

            foreach (Entity attacker in attackers)
            {
                var defenders = new List<Entity>(myEntities);
                defenders.Add(myBossEntity);

                int rand = Random.Range(0, defenders.Count);
                Attack(attacker, defenders[rand]);

                if (TurnManager.Instance.isLoading)
                    yield break;

                yield return new WaitForSeconds(2);
            }

            TurnManager.Instance.EndTurn();
        }

        void EntityAligenment(bool isMine)
        {
            float targetY = isMine ? -4.35f : 4.15f;
            var targetEntities = isMine ? myEntities : otherEntities;

            for (int i = 0; i < targetEntities.Count; i++)
            {
                //중요 피라미드와 같은 모습으로 정렬됨
                /* 
                 *          x   첫번째
                 *         x x  두번째
                 *        x x x  
                 */

                float targetX = (targetEntities.Count - 1) * -3.4f + i * 6.8f;

                var targetEntity = targetEntities[i];
                targetEntity.originPos = new Vector3(targetX, targetY, 0);
                targetEntity.MoveTransform(targetEntity.originPos, true, 0.5f);
                //MyEmptyEntity는 Order가 존재하지않는다 컴포넌트의 존재여부의 체크가 필요하다
                targetEntity.GetComponent<Order>()?.SetOriginOrder(i);
            }
        }

        public void InsertMyEmptyEntity(float xPos)
        {
            if (IsFullMyEntities)
                return;

            //초기 myEntities는 복사할 buffer의 역할의 myEmptyEntity가 존재하지않기에 
            //myEmptyEntity를 추가한다.
            if (!ExistMyEmptyEntity)
                myEntities.Add(myEmptyEntity);

            //emptyEnityPos는 새로 생성한 Entity의 좌표이다.
            //기준이 myEmptyEntitiy이기에 처음 초기화를  emptyEntityPos = myEmptyEntity.transform.position 로 진행한다
            Vector3 emptyEntityPos = myEmptyEntity.transform.position;
            //하지만 새로운 enpty의 x좌표는 다르기에 파라미터로 xPos만을 가져온다
            emptyEntityPos.x = xPos;
            //완성한 emptyEntityPos를 myEmptyEntitiy에 초기화한다
            myEmptyEntity.transform.position = emptyEntityPos;

            int _emptyEntityIndex = MyEmptityIndex;
            //x만을 비교한다 작을순부터 큰순서대로 정렬이 이루어진다
            myEntities.Sort((entity1, entity2) => entity1.transform.position.x.CompareTo(entity2.transform.position.x));
            //index의 수정이 일어났는지 체크한다 이유는 수정이 일어나지않았다면 정렬을 할 필요가없기때문이다
            if (MyEmptityIndex != _emptyEntityIndex)
                EntityAligenment(true);

        }

        public void RemoveMyEmptyEntity()
        {
            if (!ExistMyEmptyEntity)
                return;

            myEntities.RemoveAt(MyEmptityIndex);
            //삭제된 필드의 Entities를 재 정렬한다
            EntityAligenment(true);
        }

        public bool SpawnEntity(bool isMine, CardItem item, Vector3 spawnPos)
        {
            if (isMine)
            {
                if (IsFullMyEntities || !ExistMyEmptyEntity)
                    return false;
            }
            else
            {
                if (IsFullOtherEntites)
                    return false;
            }

            var entityObject = Instantiate(entityPrefab, spawnPos, Util.QI);
            var entity = entityObject.GetComponent<Entity>();

            if (isMine)
                myEntities[MyEmptityIndex] = entity;
            else
                otherEntities.Insert(Random.Range(0, otherEntities.Count), entity);

            entity.isMine = isMine;
            entity.Setup(item);
            EntityAligenment(isMine);
            return true;

        }

        public void EntityMouseDown(Entity entity)
        {
            if (!CanMosueInput)
                return;

            selectEntity = entity;
        }

        public void EntityMouseUp()
        {
            if (!CanMosueInput)
                return;

            if (selectEntity && targetPickEntity && selectEntity.attackable)
                Attack(selectEntity, targetPickEntity);

            selectEntity = null;
            targetPickEntity = null;
        }

        public void EntityMosueDrag()
        {
            if (!CanMosueInput || selectEntity == null)
                return;

            //other 타겟엔티티 찾기
            bool existTarget = false;
            foreach (var hit in Physics2D.RaycastAll(Util.MousePos, Vector3.forward))
            {
                Entity entity = hit.collider?.GetComponent<Entity>();

                if (entity != null && !entity.isMine && selectEntity.attackable)
                {
                    targetPickEntity = entity;
                    existTarget = true;
                    break;
                }
            }

            if (!existTarget)
                targetPickEntity = null;
        }

        public void AttackableReset(bool isMine)
        {
            var targetEntites = isMine ? myEntities : otherEntities;
            //존재하는 모든 attackable을 true로 초기화
            targetEntites.ForEach(x => x.attackable = true);
        }

        void Attack(Entity attacker, Entity defender)
        {
            attacker.attackable = false;
            attacker.GetComponent<Order>().SetMostFrontOrder(true);

            Sequence sequence = DOTween.Sequence()
                //1. 공격자는 방어자에게 0.4초 동안 이동한다 InSine의 굴곡처럼 행동한다
                .Append(attacker.transform.DOMove(defender.originPos, 0.4f)).SetEase(Ease.InSine)
                //2. '1'이 끝나면 호출된다
                .AppendCallback(() =>
                {
                    attacker.Damaged(defender.attack);
                    defender.Damaged(attacker.attack);
                    SpawnDamage(defender.attack, attacker.transform);
                    SpawnDamage(attacker.attack, defender.transform);
                })
                //3. 공격자의 좌표를 0.4초 동안 원래 좌표로 이동한다
                .Append(attacker.transform.DOMove(attacker.originPos, 0.4f)).SetEase(Ease.OutSine)
                .OnComplete(() => { AttackCallback(attacker, defender); }); //죽음처리
        }

        void AttackCallback(params Entity[] entities)
        {
            entities[0].GetComponent<Order>().SetMostFrontOrder(false);

            foreach (Entity entity in entities)
            {
                if (!entity.isDie | entity.isBossOrEmpty)
                    continue;

                if (entity.isMine)
                    myEntities.Remove(entity);
                else
                    otherEntities.Remove(entity);

                Sequence sequnce = DOTween.Sequence()
                    .Append(entity.transform.DOShakePosition(1.3f))
                    .Append(entity.transform.DOScale(Vector3.zero, 0.3f)).SetEase(Ease.OutCirc)
                    .OnComplete(() =>
                    {
                        EntityAligenment(entity.isMine);
                        Destroy(entity.gameObject);
                    });
            }
            StartCoroutine(CheckBossDie());
        }

        public void SpawnDamage(int damage, Transform tr)
        {
            if (damage <= 0)
                return;

            var damageComponent = Instantiate(damagePrefab).GetComponent<Damage>();
            damageComponent.SetupTransform(tr);
            damageComponent.Damaged(damage);
        }

        IEnumerator CheckBossDie()
        {
            yield return delay2;

            if (myBossEntity.isDie)
                StartCoroutine(GameManager.Instance.GameOver(false));

            if (OtherBossEntity.isDie)
                StartCoroutine(GameManager.Instance.GameOver(true));
        }

        public void DamageBoss(bool isMine, int damage)
        {
            var targetBossEntity = isMine ? myBossEntity : OtherBossEntity;
            targetBossEntity.Damaged(damage);
            StartCoroutine(CheckBossDie());
        }
    }
}