namespace AttackCooldownChangerSystem
{
    public sealed class AttackCooldownChanger
    {
        private readonly CooldownProcessor _cooldownProcessor;
        
        public AttackCooldownChanger(CooldownProcessor cooldownProcessor)
        {
            _cooldownProcessor = cooldownProcessor;
        }
        
        /// <param name="scale">
        ///     Positive scale mean decrease <br/>
        ///     Negative scale mean increase <br/>
        ///     If scale == 0, then value dont change
        /// </param>
        public void Apply(float scale)
        {
            var currentValue =  _cooldownProcessor.CooldownMaxValue * (1 + scale);
            _cooldownProcessor.SetCooldownTime(currentValue);
        }

        public void DeApply(float scale)
        {
            var currentValue = _cooldownProcessor.CooldownMaxValue / (1 + scale);
            _cooldownProcessor.SetCooldownTime(currentValue);   
        }
    }
}