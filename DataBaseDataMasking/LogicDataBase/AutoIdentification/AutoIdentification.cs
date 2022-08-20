using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataBaseDataMasking.DataBase.Base;

namespace DataBaseDataMasking.LogicDataBase.AutoIdentification
{
   public class AutoIdentification : IDisposable
    {
        public DataMaskingContext DataMaskingContext { get; set; }
        public AutoIdentification()
        {
            DataMaskingContext = new DataMaskingContext();
        }

        

        /// <summary>
        /// Disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DataMaskingContext?.Dispose();
                DataMaskingContext = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
