using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EAP.ModelFirst.Core
{
    public interface IZoomable
    {
        event EventHandler ZoomChanged;

        float Zoom { get; }

        void ChangeZoom(float zoom);

        void ZoomIn();

        void ZoomOut();

        void AutoZoom();
    }
}
