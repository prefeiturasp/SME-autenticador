namespace Autenticador.QuartzProvider.Domain
{
    using System.Collections.Generic;

    public class TriggerGroupData : ActivityNode<TriggerData>
    {
        public TriggerGroupData(string name) : base(name)
        {
        }

        public IList<TriggerData> Triggers { get; private set; }

        protected override IList<TriggerData> ChildrenActivities
        {
            get { return Triggers; }
        }
    }
}