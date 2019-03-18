using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWpf
{
    class Class1 : INotifyPropertyChanged
    {
        private string _test;

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        public Class1()
        {
            Task.Run(async () =>
            {
                int i = 0;

                while (true)
                {
                    await Task.Delay(200);
                    Test2 = (i++).ToString();
                }
            });
        }

        // same as test using the weaver
        public string Test2 { get; set; }

        public string Test {
            get
            {
                return _test;
            }
            set
            {
                if (_test == value)
                    return;

                _test = value;
                PropertyChanged(this, new PropertyChangedEventArgs(nameof(Test)));
            }
        }

        public override string ToString()
        {
            return "einai makaronia";
        }
    }
}
