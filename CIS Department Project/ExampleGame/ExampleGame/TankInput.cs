namespace ExampleGame
{
    struct TankInput
    {
        /// <summary>
        /// Represents forward and backward movement of the tank. Positive floats are forward, 
        /// negative floats are backward.
        /// </summary>
        public float ForwardBackward { get; set; }

        /// <summary>
        /// Represents left and right movement of the tank. Positive floats are right, negative floats
        /// are left.
        /// </summary>
        public float LeftRight { get; set; }

        /// <summary>
        /// Represents up and down movement of the turret.  Positive floats are down,
        /// negative floats are up
        /// </summary>
        public float TurretUpDown { get; set; }

        /// <summary>
        /// Represents left and right movement of the turret.  Positive floats are right,
        /// negative floats are left
        /// </summary>
        public float TurretLeftRight { get; set; }

        /// <summary>
        /// True if a shot has been fired, false otherwise
        /// </summary>
        public bool Shoot { get; set; }
    }
}