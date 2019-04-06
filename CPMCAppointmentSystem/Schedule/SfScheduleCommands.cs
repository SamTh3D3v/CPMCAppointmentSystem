using Syncfusion.UI.Xaml.Schedule;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMCAppointmentSystem.ScheduleControl.Commands
{
    #region Base

    public class SfScheduleCommandBase<TBehavior> : ControlCommandBase<TBehavior, Syncfusion.UI.Xaml.Schedule.SfSchedule> where TBehavior : CommandBehaviorBase<Syncfusion.UI.Xaml.Schedule.SfSchedule>, new()
    { }

    public class SfScheduleCommandBehaviorBase<TReturn, TEventArgs> : BuilderCommandBehaviorBase<Syncfusion.UI.Xaml.Schedule.SfSchedule, TEventArgs, TReturn>
    { }

    #endregion

    #region AppointmentEditorOpening

    public class AppointmentEditorOpeningCommand<T, TBehavior> : SfScheduleCommandBase<TBehavior> where TBehavior : AppointmentEditorOpeningCommandBehavior<T>, new()
    { }
    public class AppointmentEditorOpeningCommandBehavior<TReturn> : SfScheduleCommandBehaviorBase<TReturn, AppointmentEditorOpeningEventArgs>
    {
        public AppointmentEditorOpeningCommandBehavior(Func<object, AppointmentEditorOpeningEventArgs, TReturn> builder)
        {
            this.builder = builder;
        }

        public AppointmentEditorOpeningCommandBehavior()
            : this(null)
        { }

        protected override void OnTargetAttached()
        {
            TargetObject.AppointmentEditorOpening += OnEventRaised;
        }
    }
    public class AppointmentEditorOpeningCommand : SfScheduleCommandBase<AppointmentEditorOpeningCommandBehavior>
    { }

    public class AppointmentEditorOpeningCommandBehavior : AppointmentEditorOpeningCommandBehavior<object>
    { }

    public class AppointmentEditorOpeningCommandWithEventArgs : AppointmentEditorOpeningCommand<AppointmentEditorOpeningEventArgs, AppointmentEditorOpeningCommandBehaviorEventArgs>
    { }

    public class AppointmentEditorOpeningCommandBehaviorEventArgs : AppointmentEditorOpeningCommandBehavior<AppointmentEditorOpeningEventArgs>
    {
        public AppointmentEditorOpeningCommandBehaviorEventArgs()
            : base((o, e) => e)
        { }
    }

    #endregion

    #region AppointmentEditorClosing

    public class AppointmentEditorClosingCommand<T, TBehavior> : SfScheduleCommandBase<TBehavior> where TBehavior : AppointmentEditorClosingCommandBehavior<T>, new()
    { }
    public class AppointmentEditorClosingCommandBehavior<TReturn> : SfScheduleCommandBehaviorBase<TReturn, AppointmentEditorClosedEventArgs>
    {
        public AppointmentEditorClosingCommandBehavior(Func<object, AppointmentEditorClosedEventArgs, TReturn> builder)
        {
            this.builder = builder;
        }

        public AppointmentEditorClosingCommandBehavior()
            : this(null)
        { }

        protected override void OnTargetAttached()
        {
            TargetObject.AppointmentEditorClosed += OnEventRaised;
        }
    }
    public class AppointmentEditorClosingCommand : SfScheduleCommandBase<AppointmentEditorClosingCommandBehavior>
    { }

    public class AppointmentEditorClosingCommandBehavior : AppointmentEditorClosingCommandBehavior<object>
    { }

    public class AppointmentEditorClosingCommandWithEventArgs : AppointmentEditorClosingCommand<AppointmentEditorClosedEventArgs, AppointmentEditorClosingCommandBehaviorEventArgs>
    { }

    public class AppointmentEditorClosingCommandBehaviorEventArgs : AppointmentEditorClosingCommandBehavior<AppointmentEditorClosedEventArgs>
    {
        public AppointmentEditorClosingCommandBehaviorEventArgs()
            : base((o, e) => e)
        { }
    }

    #endregion

    #region ContextMenuOpening

    public class ContextMenuOpeningCommand<T, TBehavior> : SfScheduleCommandBase<TBehavior> where TBehavior : ContextMenuOpeningCommandBehavior<T>, new()
    { }
    public class ContextMenuOpeningCommandBehavior<TReturn> : SfScheduleCommandBehaviorBase<TReturn, ContextMenuOpeningEventArgs>
    {
        public ContextMenuOpeningCommandBehavior(Func<object, ContextMenuOpeningEventArgs, TReturn> builder)
        {
            this.builder = builder;
        }

        public ContextMenuOpeningCommandBehavior()
            : this(null)
        { }

        protected override void OnTargetAttached()
        {
            TargetObject.ContextMenuOpening += OnEventRaised;
        }
    }
    public class ContextMenuOpeningCommand : SfScheduleCommandBase<ContextMenuOpeningCommandBehavior>
    { }

    public class ContextMenuOpeningCommandBehavior : ContextMenuOpeningCommandBehavior<object>
    { }

    public class ContextMenuOpeningCommandWithEventArgs : ContextMenuOpeningCommand<ContextMenuOpeningEventArgs, ContextMenuOpeningCommandBehaviorEventArgs>
    { }

    public class ContextMenuOpeningCommandBehaviorEventArgs : ContextMenuOpeningCommandBehavior<ContextMenuOpeningEventArgs>
    {
        public ContextMenuOpeningCommandBehaviorEventArgs()
            : base((o, e) => e)
        { }
    }

    #endregion

    #region ContextMenuClosed

    public class ContextMenuClosingCommand<T, TBehavior> : SfScheduleCommandBase<TBehavior> where TBehavior : ContextMenuClosingCommandBehavior<T>, new()
    { }
    public class ContextMenuClosingCommandBehavior<TReturn> : SfScheduleCommandBehaviorBase<TReturn, ContextMenuClosedEventArgs>
    {
        public ContextMenuClosingCommandBehavior(Func<object, ContextMenuClosedEventArgs, TReturn> builder)
        {
            this.builder = builder;
        }

        public ContextMenuClosingCommandBehavior()
            : this(null)
        { }

        protected override void OnTargetAttached()
        {
            TargetObject.ContextMenuClosed += OnEventRaised;
        }
    }
    public class ContextMenuClosingCommand : SfScheduleCommandBase<ContextMenuClosingCommandBehavior>
    { }

    public class ContextMenuClosingCommandBehavior : ContextMenuClosingCommandBehavior<object>
    { }

    public class ContextMenuClosingCommandWithEventArgs : ContextMenuClosingCommand<ContextMenuClosedEventArgs, ContextMenuClosingCommandBehaviorEventArgs>
    { }

    public class ContextMenuClosingCommandBehaviorEventArgs : ContextMenuClosingCommandBehavior<ContextMenuClosedEventArgs>
    {
        public ContextMenuClosingCommandBehaviorEventArgs()
            : base((o, e) => e)
        { }
    }

    #endregion

    #region VisibleDatesChanging

    public class VisibleDatesChangingCommand<T, TBehavior> : SfScheduleCommandBase<TBehavior> where TBehavior : VisibleDatesChangingCommandBehavior<T>, new()
    { }
    public class VisibleDatesChangingCommandBehavior<TReturn> : SfScheduleCommandBehaviorBase<TReturn, VisibleDatesChangingEventArgs>
    {
        public VisibleDatesChangingCommandBehavior(Func<object, VisibleDatesChangingEventArgs, TReturn> builder)
        {
            this.builder = builder;
        }

        public VisibleDatesChangingCommandBehavior()
            : this(null)
        { }

        protected override void OnTargetAttached()
        {
            TargetObject.VisibleDatesChanging += OnEventRaised;
        }
    }
    public class VisibleDatesChangingCommand : SfScheduleCommandBase<VisibleDatesChangingCommandBehavior>
    { }

    public class VisibleDatesChangingCommandBehavior : VisibleDatesChangingCommandBehavior<object>
    { }

    public class VisibleDatesChangingCommandWithEventArgs : VisibleDatesChangingCommand<VisibleDatesChangingEventArgs, VisibleDatesChangingCommandBehaviorEventArgs>
    { }

    public class VisibleDatesChangingCommandBehaviorEventArgs : VisibleDatesChangingCommandBehavior<VisibleDatesChangingEventArgs>
    {
        public VisibleDatesChangingCommandBehaviorEventArgs()
            : base((o, e) => e)
        { }
    }

    #endregion
}
