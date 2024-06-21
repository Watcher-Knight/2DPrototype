using System.Collections.Generic;
using UnityEngine.EventSystems;
namespace Nikusoft.CheckPoints
{
    public interface ICheckPoint
    {
        public void InitArea();
        public List<EventTag> TriggerEvents();
        public void ValidateCheckPoint();

    }
}