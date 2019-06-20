namespace TowerDefend
{
    public class Constants
    {

        public const int GEM_REWARD = 50;

        public const int POOLER_LENGTH = 1;
        public const float GRAVITATIONAL_ACCELERATION = 39.3f;
        public const int LEVEL_COUNT = 49;

        #region Enemy
        public const int AMOUNT_HEALTH_INCREASE_PER_WAVE = 5;

        //------runner enemy(lau la)
        public const float SPEED_RUNNER = 1.75f;
        public const float HEATH_RUNNER = 60;
        public const float ARMOR_RUNNER = 0;
        public const int REWARD_RUNNER = 2;

        //------darkwarrior enemy(ky si bong dem)
        public const float SPEED_DARKWARRIOR = 1.25f;
        public const float HEATH_DARKWARRIOR = 125;
        public const float ARMOR_DARKWARRIOR = 4;
        public const int REWARD_DARKWARRIOR = 5;

        //-----butcher enemy(do te)
        public const float SPEED_BUTCHER = 0.75f;
        public const float HEATH_BUTCHER = 175;
        public const float ARMOR_BUTCHER = 8;
        public const int REWARD_BUTCHER = 10;

        //-----ghost enemy(hon ma)
        public const float SPEED_GHOST = 0.75f;
        public const float HEATH_GHOST = 100;
        public const float ARMOR_GHOST = 0;
        public const float INVISIBLE_WAIT_TIME_GHOST = 3;
        public const int REWARD_GHOST = 7;

        //-----hellguarder enemy(ke gac cong dia nguc)
        public const float SPEED_HELLGUARDER = 0.25f;
        public const float HEATH_HELLGUARDER = 1000;
        public const float ARMOR_HELLGUARDER = 10;
        public const int EXPLODE_DAMAGE_HELLGUARD = 100;
        public const int REWARD_HELLGUARD = 20;

        //-----darkwizard enemy(phu thuy bong dem)
        public const float SPEED_DARKWIZARD = 1.00f;
        public const float HEATH_DARKWIZARD = 100;
        public const float ARMOR_DARKWIZARD = 0;
        public const int REWARD_DARKWIZARD = 7;

        //-----necromancer enemy(ke goi hon)
        public const float SPEED_NECROMANCER = 0.50f;
        public const float HEATH_NECROMANCER = 150;
        public const float ARMOR_NECROMANCER = 0;
        public const float REVIVE_WAIT_TIME_NECROMANCER = 2f;
        public const int PERCENT_REVIVE_NECROMANCER = 30;
        public const int REWARD_NECROMANCER = 17;

        //-----UndeadWorm enemy(sau dia nguc)
        public const float SPEED_UNDEADWORM = 0.50f;
        public const float HEATH_UNDEADWORM = 150;
        public const float ARMOR_UNDEADWORM = 5;
        public const int REWARD_UNDEADWORM = 5;
        //-----shadow enemy(tieu quy)
        public const float SPEED_SHADOW = 1.00f;
        public const float HEATH_SHADOW = 100;
        public const float ARMOR_SHADOW = 0;
        public const int REWARD_SHADOW = 7;
        //-----glutondragon enemy(rong co dai)
        public const float SPEED_GLUTONDRAGON = 0.50f;
        public const float HEATH_GLUTONDRAGON = 150;
        public const float ARMOR_GLUTONDRAGON = 0;
        public const float AMOUNT_HEALTH_REGENARATE_GLUTONDRAGON = 20;
        public const int REWARD_GLUTONDRAGON = 20;

        //-----wyvern enemy(rong an thit)
        public const float SPEED_WYVERN = 0.75f;
        public const float HEATH_WYVERN = 350;
        public const float ARMOR_WYVERN = 0;
        public const int REWARD_WYVERN = 20;
        //-----yvern enemy(rong an thit)
        public const float SPEED_YVERN = 0.5f;
        public const float HEATH_YVERN = 150;
        public const float ARMOR_YVERN = 0;
        public const int REWARD_YVERN = 15;

        //-----carrier enemy(ke van chuyen)
        public const int CARIER_SPAWN_MINION_INTERVAL = 10;
        public const float SPEED_CARRIER = 0.50f;
        public const float HEATH_CARRIER = 300;
        public const float ARMOR_CARRIER = 0;
        public const int REWARD_CARRIER = 50;

        //-----tyrantdragon enemy(rong bao chua)
        public const float SPEED_TYRANTDRAGON = 0.50f;
        public const float HEATH_TYRANTDRAGON = 500;
        public const float ARMOR_TYRANTDRAGON = 4;
        public const int REWARD_TYRANTDRAGON = 50;

        //-----nightmareshipper enemy(ke gieo ac mong)
        public const float SPEED_NIGHTMARESHIPPER = 0.75f;
        public const float HEATH_NIGHTMARESHIPPER = 400;
        public const float ARMOR_NIGHTMARESHIPPER = 2;
        public const int DODGE_PERCENT_NIGHTMARESHIPPER = 30;
        public const int REWARD_NIGHTMARESHIPPER = 40;

        //-----immortaldevil enemy(ac quy bat diet)
        public const float SPEED_IMMORTALDEVIL = 0.25f;
        public const float HEATH_IMMORTALDEVIL = 3000;
        public const float ARMOR_IMMORTALDEVIL = 10;
        public const float TIME_BETWEEN_SPAWN_MINIONS_IMMORTALDEVIL = 10;
        public const int REWARD_IMMORTALDEVIL = 400;
        #endregion

        #region Bullet
        public const float ARROW_SPEED = 10f;
        public const int ARROW_DAMAGE = 30;

        public const float MORTAR_Y_VELOCITY = 18f;
        public const int MORTAR_DAMAGE = 40;


        public const float MAGIC_BALL_SPEED = 10f;
        public const int MAGIC_BALL_DAMAGE = 35;
        #endregion
        #region Tower
        public const float NORMAL_TOWER_WAIT_TIME = 1f;
        public const float NORMAL_TOWER_SPEED_MULTIPLIER = 1f;
        public const float NORMAL_TOWER_DAMAGE_MULTIPLIER = 1f;
        public const int NORMAL_TOWER_COST = 15;
        public const float NORMAL_TOWER_RANGE = 2.25f;

        public const int NORMAL_TOWER_FROZEN_LEVEL1 = 10;
        public const int NORMAL_TOWER_FROZEN_LEVEL1_COST = 15;
        public const int NORMAL_TOWER_FROZEN_LEVEL2 = 100;
        public const int NORMAL_TOWER_FROZEN_LEVEL2_COST = 20;
        public const int NORMAL_TOWER_FIRE_LEVEL1 = 11;
        public const int NORMAL_TOWER_FIRE_LEVEL1_COST = 15;
        public const int NORMAL_TOWER_FIRE_LEVEL2 = 110;
        public const int NORMAL_TOWER_FIRE_LEVEL2_COST = 20;
        public const float NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL1 = 1.5f;
        public const float NORMAL_TOWER_FIRE_DAMAGE_MULTIPLIER_LEVEL2 = 2f;

        public const float CANON_TOWER_WAIT_TIME = 3f;
        public const int CANON_TOWER_COST = 50;
        public const float CANON_TOWER_DAMAGE_MULTIPLIER = 1f;
        public const float CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL2 = 2f;
        public const int CANON_TOWER_LEVEL2_COST = 50;
        public const float CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL3 = 2.5f;
        public const int CANON_TOWER_LEVEL3_COST = 95;
        public const float CANON_TOWER_DAMAGE_MULTIPLIER_LEVEL4 = 3f;
        public const int CANON_TOWER_LEVEL4_COST = 100;

        public const float LONG_RANGE_TOWER_WAIT_TIME = 1f;
        public const int LONG_RANGE_TOWER_COST = 45;
        public const float LONG_RANGE_TOWER_DAMAGE_MULTIPLIER = 2f;
        public const float LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL2 = 2.333f;
        public const int LONG_RANGE_TOWER_COST_LEVEL2 = 45;
        public const float LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL20 = 3f;
        public const int LONG_RANGE_TOWER_COST_LEVEL20 = 75;
        public const float LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL21 = 3f;
        public const int LONG_RANGE_TOWER_COST_LEVEL21 = 75;
        public const float LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL210 = 3.333f;
        public const int LONG_RANGE_TOWER_COST_LEVEL210 = 125;
        public const float LONG_RANGE_TOWER_DAMAGE_MULTIPLIER_LEVEL200 = 3.333f;
        public const int LONG_RANGE_TOWER_COST_LEVEL200 = 125;
        public const float LONG_RANGE_TOWER_SPEED_MULTIPLIER = 2f;

        public const float MAGIC_TOWER_WAIT_TIME = 2f;
        public const int MAGIC_TOWER_COST = 35;
        public const float MAGIC_TOWER_DAMAGE_MULTIPLIER = 1f;
        public const float MAGIC_TOWER_SPEED_MULTIPLIER = 1f;
        public const float MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL2 = 1.33f;
        public const float MAGIC_TOWER_SPEED_MULTIPLIER_LEVEL2 = 1.5f;
        public const int MAGIC_TOWER_COST_LEVEL2 = 35;
        public const float MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL20 = 1.75f;
        public const float MAGIC_TOWER_SPEED_MULTIPLIER_LEVEL20 = 1.5f;
        public const int MAGIC_TOWER_COST_LEVEL20 = 100;
        public const int MAGIC_TOWER_COST_LEVEL21 = 100;
        public const int MAGIC_TOWER_COST_LEVEL210 = 125;
        public const int MAGIC_TOWER_COST_LEVEL200 = 125;
        public const float MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL200 = 1.75f;
        public const float MAGIC_TOWER_SPEED_MULTIPLIER_LEVEL200 = 1.33f;
        public const float MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL21 = 1.5f;
        public const float MAGIC_TOWER_SPEED_MULTIPLIER_LEVEL21 = 1f;
        public const float MAGIC_TOWER_DAMAGE_MULTIPLIER_LEVEL210 = 1.5f;
        public const float MAGIC_TOWER_SPEED_MULTIPLIER_LEVEL210 = 1.5f;
        public const float MAGIC_TOWER_SHOOT_PLAYING_TIME = 0.5f;
        public const float MAGIC_TOWER_RANGE = 2.25f;


        public const float SOUL_TOWER_ATTACK_SPEED_BONUS = 1.25f;
        public const int SOUL_TOWER_COST = 60;
        public const float SOUL_TOWER_ATTACK_DAMAGE_BONUS = 1.25f;
        public const float SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL2 = 1.25f;
        public const float SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL2 = 1.25f;
        public const int SOUL_TOWER_COST_LEVEL2 = 60;
        public const float SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL20 = 1.5f;
        public const float SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL20 = 1.5f;
        public const int SOUL_TOWER_COST_LEVEL20 = 90;
        public const float SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL21 = 1.5f;
        public const float SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL21 = 1.5f;
        public const int SOUL_TOWER_COST_LEVEL21 = 90;
        public const float SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL200 = 1.5f;
        public const float SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL200 = 1.5f;
        public const int SOUL_TOWER_COST_LEVEL200 = 160;
        public const float SOUL_TOWER_ATTACK_SPEED_BONUS_LEVEL210 = 1.5f;
        public const float SOUL_TOWER_ATTACK_DAMAGE_BONUS_LEVEL210 = 1.5f;
        public const int SOUL_TOWER_COST_LEVEL210 = 160;
        public const float SOUL_TOWER_RANGE_MULTIPLY = 1.5f;
        #endregion
        #region Skill
        public const int MINE_EXPLODE_WAIT_TIME = 3;
        public const int TIMESKILLDURATION = 7;
        public const int FROZENSKILLDURATION = 5;
        public const int METEOR_DAMAGE = 250;
        public const int MINE_DAMAGE = 150;
        public const int TIME_COST = 70;
        public const int FROZEN_COST = 40;
        public const int MINE_COST = 60;
        public const int METEOR_COST = 90;

        #endregion
        public const float BLOOD_PLAYING_TIME = 0.25f;
        public const int TIME_BETWEEN_WAVE = 15;

        #region Trap
        public const int TRAP_WAIT_TIME = 1;
        public const int TRAP_DAMAGE = 8;
        public const float ICE_TRAP_FREEZE = 0.8f;
        public const int FIRE_TRAP_AMOR_PIERCE = 5;
        #endregion
        #region Sounds
        public const int TOWER_PLACED = 0;
        public const int BUTTON_CLICK = 1;
        public const int UPGRADE_SUCCESSFULL = 2;
        #endregion
        #region Upgrade

        public const int NUMBER_OF_UPGRADE = 24;

        public const float STARTING_GOLD_MULTIPLY = 1.2f;
        public const int UPGRADE_STARTING_GOLD_COST = 6;
        public const int UPGRADE_STARTING_GOLD_INDEX = 0;

        public const int UPGRADE_BONUS_GOLD_COST = 9;
        public const float BONUS_GOLD_MULTIPLY = 1.05f;
        public const int UPGRADE_BONUS_GOLD_INDEX = 1;

        public const int UPGRADE_METEOR_AREA_COST = 3;
        public const int UPGRADE_METEOR_AREA_INDEX = 2;
        public static int METEOR_AREA_MULTIPLY = 1;

        public const int UPGRADE_METEOR_DAMAGE_COST = 6;
        public static int METEOR_DAMAGE_MULTIPLY = 1;
        public const int UPGRADE_METEOR_DAMAGE_INDEX = 3;

        public const int UPGRADE_FREEZE_DURATION_COST = 4;
        public static int UPGRADE_FREEZE_DURATION_TIME = 4;


        public const int UPGRADE_VOID_DURATION_COST = 4;
        public static int UPGRADE_VOID_DURATION_TIME = 4;


        public const int UPGRADE_FAST_COOLDOWN_COST = 9;
        public static float UPGRADE_FAST_COOLDOWN_MULTIPLY = 0.5f;

        public const int UPGRADE_STARTING_MANA_COST = 6;
        public const int UPGRADE_STARTING_MANA_INDEX = 7;
        public const float UPGRADE_STARTING_MANA_MULTIPLY = 1.5f;

        public const int UPGRADE_BASIC_COST_COST = 3;
        public const int UPGRADE_BASIC_COST_INDEX = 8;
        public const float UPGRADE_BASIC_COST_MULTIPLY = 0.7f;

        public const int UPGRADE_BASIC_ATTACK_SPEED_COST = 5;
        public const int UPGRADE_BASIC_ATTACK_SPEED_INDEX = 9;
        public const float UPGRADE_BASIC_ATTACK_SPEED_MULTIPLY = 0.8f;

        public const int UPGRADE_BASIC_DOUBLE_DAMAGE_COST = 7;
        public const int UPGRADE_BASIC_DOUBLE_DAMAGE_INDEX = 10;
        public static float UPGRADE_BASIC_DOUBLE_DAMAGE_PERCENT = 15;

        public const int UPGRADE_BASIC_RANGE_COST = 10;
        public const int UPGRADE_BASIC_RANGE_INDEX = 11;
        public const float UPGRADE_BASIC_RANGE_MULTIPLY = 1.4f;

        public const int UPGRADE_CANON_MAX_DAMAGE_COST = 3;
        public static float UPGRADE_CANON_MAX_DAMAGE_MULTIPLY = 3;

        public const int UPGRADE_CANON_ATTACK_DAMAGE_COST = 5;
        public const int UPGRADE_CANON_ATTACK_DAMAGE_INDEX = 13;
        public const float UPGRADE_CANON_ATTACK_DAMAGE_MULTIPLY = 1.15f;

        public const int UPGRADE_CANON_ATTACK_SPEED_COST = 7;
        public const int UPGRADE_CANON_ATTACK_SPEED_INDEX = 14;
        public const float UPGRADE_CANON_ATTACK_SPEED_MULTIPLY = 0.85f;

        public const int UPGRADE_CANON_MINI_STUN_COST = 10;
        public static float UPGRADE_CANON_MINI_STUN_MULTIPLY = 10;

        public const int UPGRADE_ARCHER_ATTACK_SPEED_COST = 3;
        public const int UPGRADE_ARCHER_ATTACK_SPEED_INDEX = 16;
        public const float UPGRADE_ARCHER_ATTACK_SPEED_MULTIPLY = 0.85f;

        public const int UPGRADE_ARCHER_ATTACK_DAMAGE_COST = 5;
        public const int UPGRADE_ARCHER_ATTACK_DAMAGE_INDEX = 17;
        public const float UPGRADE_ARCHER_ATTACK_DAMAGE_MULTIPLY = 1.15f;

        public const int UPGRADE_ARCHER_COST_COST = 7;
        public const int UPGRADE_ARCHER_COST_INDEX = 18;
        public const float UPGRADE_ARCHER_COST_MULTIPLY = 0.85f;

        public const int UPGRADE_ARCHER_DOUBLE_ARROW_COST = 10;
        public const int UPGRADE_ARCHER_DOUBLE_ARROW_INDEX = 19;
        public static float UPGRADE_ARCHER_DOUBLE_ARROW_MULTIPLY = 10;

        public const int UPGRADE_MAGIC_CHAOS_DAMAGE_COST = 3;
        public static float UPGRADE_MAGIC_CHAOS_DAMAGE_MULTIPLY = 3;

        public const int UPGRADE_MAGIC_RANGE_COST = 5;
        public const int UPGRADE_MAGIC_RANGE_INDEX = 21;
        public const float UPGRADE_MAGIC_RANGE_MULTIPLY = 1.25f;

        public const int UPGRADE_MAGIC_DAMAGE_COST = 7;
        public const int UPGRADE_MAGIC_DAMAGE_INDEX = 22;
        public const float UPGRADE_MAGIC_DAMAGE_MULTIPLY = 1.1f;

        public const int UPGRADE_MAGIC_UNITED_COST = 10;
        public const int UPGRADE_MAGIC_UNITED_INDEX = 23;
        public const float UPGRADE_MAGIC_UNITED_MULTIPLY = 10;

        #endregion

        
    }
}