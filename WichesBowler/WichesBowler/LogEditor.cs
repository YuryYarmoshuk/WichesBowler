using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WichesBowler
{
    public interface IStore
    {
        void Save(string logMsg);
    }

    public class LogEditor
    {
        IStore _store;

        public LogEditor()
        {

        }

        public LogEditor(IStore store)
        {
            _store = store;
        }

        public string LogStrEditor(string[] mas)
        {
            string res = "";

            if (_store != null)
            {
                _store.Save(res);
            }

            return res;
        }
    }
}
