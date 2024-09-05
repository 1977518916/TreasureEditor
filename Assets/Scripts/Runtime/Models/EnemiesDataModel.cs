using System.Collections.Generic;
using QFramework;
using Runtime.Utils;

namespace Runtime.Data
{
    /// <summary>
    /// 所有波敌人的数据
    /// </summary>
    public class EnemiesDataModel : AbstractModel
    {
        private List<TimesData> timesData;
        private List<TimesData> TimesData
        {
            get
            {
                return timesData ??= new List<TimesData>();
            }
            set
            {
                timesData = value;
                this.GetUtility<ReadWriteUtility>().Write(nameof(EnemiesDataModel), timesData);
            }
        }
        protected override void OnInit()
        {
            timesData = this.GetUtility<ReadWriteUtility>().Read(nameof(EnemiesDataModel), new List<TimesData>()
            {
                new TimesData()
            });
        }
    }

}