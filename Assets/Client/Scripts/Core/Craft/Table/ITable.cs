using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Client.Scripts.Core.Craft.Table
{
    public interface ITable
    {
        public void Start();
        public void Stop();
        public void SetTimer();
    }
    public delegate int TimerTable();
}
