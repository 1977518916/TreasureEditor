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
        private List<EnemyMakerData> timesData;
        private List<EnemyMakerData> TimesData
        {
            get
            {
                return timesData ??= new List<EnemyMakerData>();
            }
            set
            {
                timesData = value;
                this.GetUtility<ReadWriteUtility>().Write(nameof(EnemiesDataModel), timesData);
            }
        }
        protected override void OnInit()
        {
            timesData = this.GetUtility<ReadWriteUtility>().Read(nameof(EnemiesDataModel), new List<EnemyMakerData>()
            {
                new EnemyMakerData()
            });
        }
    }

}