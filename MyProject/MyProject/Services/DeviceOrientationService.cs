using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Service;

public partial class DeviceOrientationService
{
    public QueueBuffer SerialBuffer = new();

    public partial void ConfigureScanner();
    public class QueueBuffer : Queue
    {
        public event EventHandler? Changed;
        public override void Enqueue(object? obj)
        {
            base.Enqueue(obj);
            Changed?.Invoke(this, EventArgs.Empty);
        }
    }

}
