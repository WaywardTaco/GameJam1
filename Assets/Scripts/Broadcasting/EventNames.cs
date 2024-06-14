using UnityEngine;
using System.Collections;

/*
 * Holder for event names
 * Created By: NeilDG
 */ 
public class EventNames {
	public class Powerups {
		public const string ON_COLLIDE_CANISTER = "ON_COLLIDE_CANISTER";
		public const string ON_COLLIDE_CAPSULE = "ON_COLLIDE_CAPSULE";
        public const string ON_COLLIDE_JETPACK = "ON_COLLIDE_JETPACK";
    }

	public class Checkpoint {
        public const string ON_COLLIDE_CHECKPOINT = "ON_COLLIDE_CHECKPOINT";
    }

    public class MistCollide 
    {
        public const string ON_COLLIDE_MIST = "ON_COLLIDE_MIST";
        public const string ON_MIST_DAMAGE = "ON_MIST_DAMAGE";
        public const string ON_MIST_EXIT = "ON_MIST_EXIT";
        public const string ON_MIST_KILLS = "ON_MIST_KILLS";
    }
}







