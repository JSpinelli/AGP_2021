using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnBaseGame : MonoBehaviour
{
    private TaskManager _tm = new TaskManager();


    public List<Combatant> team1 = new List<Combatant>();
    public List<Combatant> team2 = new List<Combatant>();
    private Combatant party1;
    private Combatant party2;
    private Combatant enemy1;
    private Combatant enemy2;

    private Combatant[] currentTurn;
    private Task roundOutcome;
    private Task currentPointer;

    private int turnCounter = 0;

    void Start()
    {
        party1 = new Combatant(this, "HERO", 50, 20, team1, team2);
        party2 = new Combatant(this, "BEST FRIEND", 80, 10, team1, team2);
        enemy1 = new Combatant(this, "BAD DUDE DPS", 20, 30, team2, team1);
        enemy2 = new Combatant(this, "BAD DUDE TANK", 80, 10, team2, team1);

        team1.Add(party1);
        team1.Add(party2);
        team2.Add(enemy1);
        team2.Add(enemy2);

        roundOutcome = null;
        currentTurn = new[] {party1, party2, enemy1, enemy2};
        
        Debug.Log("Current Turn: "+ currentTurn[turnCounter].name+" A to attack, H to heal");
    }

    private void Status()
    {
        Debug.Log("--- STATUS OF YOUR GROUP ---");
        foreach (var ally in team1)
        {
            Debug.Log(ally.name);
            Debug.Log("HEALTH: " + ally.health + " ATTACK POWER: " + ally.attackPower);
        }

        Debug.Log("--- STATUS OF THE ENEMY ---");
        foreach (var enemy in team2)
        {
            Debug.Log(enemy.name);
            Debug.Log("HEALTH: " + enemy.health + " ATTACK POWER: " + enemy.attackPower);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Status();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            DelegateTask attackTask = new DelegateTask(currentTurn[turnCounter].PickEnemyTarget, currentTurn[turnCounter].AttackTarget);
            if (roundOutcome == null)
            {
                currentPointer = roundOutcome = attackTask;
            }
            else
            {
                currentPointer.Then(attackTask);
                currentPointer = attackTask;
            }
            turnCounter++;
            if (turnCounter < currentTurn.Length)
            Debug.Log("Current Turn: "+ currentTurn[turnCounter].name+" A to attack, H to heal");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            DelegateTask healTask = new DelegateTask(currentTurn[turnCounter].PickAllyTarget, currentTurn[turnCounter].HealTeam);
            if (roundOutcome == null)
            {
                currentPointer = roundOutcome = healTask;
            }
            else
            {
                currentPointer.Then(healTask);
                currentPointer = healTask;
            }

            turnCounter++;
            if (turnCounter < currentTurn.Length)
            Debug.Log("Current Turn: "+ currentTurn[turnCounter].name+" A to attack, H to heal");
        }

        if (turnCounter == currentTurn.Length)
        {
            Debug.Log("Calculating Round");
            turnCounter = 0;
            _tm.Do(roundOutcome);
        }

        _tm.Update();
    }

    public class Combatant
    {
        private TurnBaseGame context;

        public string name;

        public float health;
        public float attackPower;

        public List<Combatant> allies;
        public List<Combatant> enemies;

        private Combatant enemyTarget;
        private Combatant allyTarget;


        public Combatant(TurnBaseGame context, string name, float health, float attackPower, List<Combatant> allies,
            List<Combatant> enemies)
        {
            this.name = name;
            this.context = context;
            this.health = health;
            this.attackPower = attackPower;
            this.allies = allies;
            this.enemies = enemies;
        }


        public void PickEnemyTarget()
        {
            int index = Random.Range(0, enemies.Count);
            enemyTarget = enemies[index];
        }

        public void PickAllyTarget()
        {
            int index = Random.Range(0, allies.Count);
            allyTarget = allies[index];
        }

        public bool HealTeam()
        {
            allyTarget.health += 10;
            Debug.Log(name + " heals " + allyTarget.name + " for " + 10);
            return true;
        }

        public bool AttackTarget()
        {
            float dmg = Random.Range(attackPower - 10, attackPower);
            enemyTarget.health -= dmg;
            Debug.Log(name + " hits " + enemyTarget.name + " for " + dmg);
            return true;
        }
    }
}