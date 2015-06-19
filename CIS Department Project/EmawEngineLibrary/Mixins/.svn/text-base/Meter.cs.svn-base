using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EmawEngineLibrary.Mixins
{
    /// <summary>
    /// meter to be used for the Hp and ammo
    /// </summary>
    public class Meter
    {
        private int m_currentValue;
        public int CurrentValue { get { return m_currentValue; } set { m_currentValue = value; } }
        public double CurrentPercent { get { return (double)m_currentValue/(double)m_maxValue; } }
        public bool IsEmpty{get { return m_currentValue < 1; } }

        private int m_maxValue;

        /// <summary>
        /// initializes m_currentValue and m_maxValue to the given int
        /// </summary>
        /// <param name="maxHealth"></param>
        public Meter(int maxValue)
        {
            m_currentValue = maxValue;
            m_maxValue = maxValue;
        }

        public Meter(int currentHealth, int maxHealth)
        {
            m_currentValue = currentHealth;
            m_maxValue = maxHealth;
        }

        public void DecreaseCurrentValue(int reduction)
        {
            m_currentValue -= reduction;
        }

        public void IncreaseCurrentValue(int increase)
        {
            m_currentValue += increase;
            if (m_currentValue > m_maxValue) m_currentValue = m_maxValue;
        }
    }
}
