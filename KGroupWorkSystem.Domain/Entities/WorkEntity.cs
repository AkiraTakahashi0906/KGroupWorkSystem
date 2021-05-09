using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGroupWorkSystem.Domain.Entities
{
    public sealed class WorkEntity
    {
        public enum ActivityName
        {
            PartsBarcodeReading=1,         //部品バーコード読み取り
            PartsShelving=2,                    //部品棚入れ
            KitRead = 3,                          //KIT読み取り
            KitOrder = 4,                          //KIT読み取り
            AutoWaitMode =6,                  //自動待機モード
        }
        public WorkEntity(int workBlock_id,
                                   string workBlockName,
                                   int workSection_id,
                                   string workSectionName,
                                   int workActivity_id,
                                   string workActivityName)
        {
            WorkBlock_id = workBlock_id;
            WorkBlockName = workBlockName;
            WorkSection_id = workSection_id;
            WorkSectionName = workSectionName;
            WorkActivity_id = workActivity_id;
            WorkActivityName = workActivityName;
        }
        public int WorkBlock_id { get; }
        public string WorkBlockName { get; }
        public int WorkSection_id { get; }
        public string WorkSectionName { get; }
        public int WorkActivity_id { get; }
        public string WorkActivityName { get; }
    }
}
