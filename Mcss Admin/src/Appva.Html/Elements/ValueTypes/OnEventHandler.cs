// <copyright file="OnEventHandler.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    /// <summary>
    /// Event handler values for <see cref="IHtmlElement{T}.On"/>.
    /// </summary>
    public enum OnEventHandler
    {
        /// <summary>
        /// Abort event.
        /// </summary>
        [Code("onabort")]
        Abort,

        /// <summary>
        /// Autocomplete event.
        /// </summary>
        [Code("onautocomplete")]
        Autocomplete,

        /// <summary>
        /// Autocomplete error event.
        /// </summary>
        [Code("onautocompleteerror")]
        AutocompleteError,

        /// <summary>
        /// Blur event.
        /// </summary>
        [Code("onblur")]
        Blur,

        /// <summary>
        /// Cancel event.
        /// </summary>
        [Code("oncancel")]
        Cancel,

        /// <summary>
        /// Can play event.
        /// </summary>
        [Code("oncanplay")]
        CanPlay,

        /// <summary>
        /// Can play through event.
        /// </summary>
        [Code("oncanplaythrough")]
        CanPlayThrough,

        /// <summary>
        /// Change event.
        /// </summary>
        [Code("onchange")]
        Change,

        /// <summary>
        /// Click event.
        /// </summary>
        [Code("onclick")]
        Click,

        /// <summary>
        /// Close event.
        /// </summary>
        [Code("onclose")]
        Close,

        /// <summary>
        /// Context menu event.
        /// </summary>
        [Code("oncontextmenu")]
        ContextMenu,

        /// <summary>
        /// Cue change event.
        /// </summary>
        [Code("oncuechange")]
        CueChange,

        /// <summary>
        /// Double click event.
        /// </summary>
        [Code("ondblclick")]
        DoubleClick,

        /// <summary>
        /// Drag event.
        /// </summary>
        [Code("ondrag")]
        Drag,

        /// <summary>
        /// Drag end event.
        /// </summary>
        [Code("ondragend")]
        DragEnd,

        /// <summary>
        /// Drag enter event.
        /// </summary>
        [Code("ondragenter")]
        DragEnter,

        /// <summary>
        /// Drag exit event.
        /// </summary>
        [Code("ondragexit")]
        Dragexit,

        /// <summary>
        /// Drag leave event.
        /// </summary>
        [Code("ondragleave")]
        Dragleave,

        /// <summary>
        /// Drag over event.
        /// </summary>
        [Code("ondragover")]
        Dragover,

        /// <summary>
        /// Drag start event.
        /// </summary>
        [Code("ondragstart")]
        Dragstart,

        /// <summary>
        /// Drop event.
        /// </summary>
        [Code("ondrop")]
        Drop,

        /// <summary>
        /// Duration change event.
        /// </summary>
        [Code("ondurationchange")]
        DurationChange,

        /// <summary>
        /// Emptied event.
        /// </summary>
        [Code("onemptied")]
        Emptied,

        /// <summary>
        /// Ended event.
        /// </summary>
        [Code("onended")]
        Ended,

        /// <summary>
        /// Error event.
        /// </summary>
        [Code("onerror")]
        Error,

        /// <summary>
        /// Focus event.
        /// </summary>
        [Code("onfocus")]
        Focus,

        /// <summary>
        /// Input event.
        /// </summary>
        [Code("oninput")]
        Input,

        /// <summary>
        /// Invalid event.
        /// </summary>
        [Code("oninvalid")]
        Invalid,

        /// <summary>
        /// Keydown event.
        /// </summary>
        [Code("onkeydown")]
        Keydown,

        /// <summary>
        /// Keypress event.
        /// </summary>
        [Code("onkeypress")]
        Keypress, 

        /// <summary>
        /// Keyup event.
        /// </summary>
        [Code("onkeyup")]
        Keyup,

        /// <summary>
        /// Load event.
        /// </summary>
        [Code("onload")]
        Load,

        /// <summary>
        /// Loaded data event.
        /// </summary>
        [Code("onloadeddata")]
        LoadedData,

        /// <summary>
        /// Loaded meta data event.
        /// </summary>
        [Code("onloadedmetadata")]
        LoadedMetaData,

        /// <summary>
        /// Load start event.
        /// </summary>
        [Code("onloadstart")]
        Loadstart,

        /// <summary>
        /// Mouse down event.
        /// </summary>
        [Code("onmousedown")]
        MouseDown,

        /// <summary>
        /// Mouse enter event.
        /// </summary>
        [Code("onmouseenter")]
        MouseEnter,

        /// <summary>
        /// Mouse leave event.
        /// </summary>
        [Code("onmouseleave")]
        MouseLeave,

        /// <summary>
        /// Mouse move event.
        /// </summary>
        [Code("on")]
        MouseMove,

        /// <summary>
        /// Mouse out event.
        /// </summary>
        [Code("onmouseout")]
        MouseOut,

        /// <summary>
        /// Mouse over event.
        /// </summary>
        [Code("onmouseover")]
        MouseOver,

        /// <summary>
        /// Mouse up event.
        /// </summary>
        [Code("onmouseup")]
        MouseUp,

        /// <summary>
        /// Mouse wheel event.
        /// </summary>
        [Code("onmousewheel")]
        MouseWheel,

        /// <summary>
        /// Pause event.
        /// </summary>
        [Code("onpause")]
        Pause,

        /// <summary>
        /// Play event.
        /// </summary>
        [Code("onplay")]
        Play,

        /// <summary>
        /// Playing event.
        /// </summary>
        [Code("onplaying")]
        Playing,

        /// <summary>
        /// Progress event.
        /// </summary>
        [Code("onprogress")]
        Progress,

        /// <summary>
        /// Rate change event.
        /// </summary>
        [Code("onratechange")]
        RateChange,

        /// <summary>
        /// Reset event.
        /// </summary>
        [Code("onreset")]
        Reset,

        /// <summary>
        /// Resize event.
        /// </summary>
        [Code("onresize")]
        Resize,

        /// <summary>
        /// Scroll event.
        /// </summary>
        [Code("onscroll")]
        Scroll,

        /// <summary>
        /// Seeked event.
        /// </summary>
        [Code("onseeked")]
        Seeked,

        /// <summary>
        /// Seeking event.
        /// </summary>
        [Code("onseeking")]
        Seeking,

        /// <summary>
        /// Select event.
        /// </summary>
        [Code("onselect")]
        Select,

        /// <summary>
        /// Show event.
        /// </summary>
        [Code("onshow")]
        Show,

        /// <summary>
        /// Sort event.
        /// </summary>
        [Code("onsort")]
        Sort,

        /// <summary>
        /// Stalled event.
        /// </summary>
        [Code("onstalled")]
        Stalled,

        /// <summary>
        /// Submit event.
        /// </summary>
        [Code("onsubmit")]
        Submit,

        /// <summary>
        /// Suspend event.
        /// </summary>
        [Code("onsuspend")]
        Suspend,

        /// <summary>
        /// Time update event.
        /// </summary>
        [Code("ontimeupdate")]
        TimeUpdate,

        /// <summary>
        /// Toggle event.
        /// </summary>
        [Code("ontoggle")]
        Toggle,

        /// <summary>
        /// Volume change event.
        /// </summary>
        [Code("onvolumechange")]
        VolumeChange,

        /// <summary>
        /// Waiting event.
        /// </summary>
        [Code("onwaiting")]
        Waiting
    }
}