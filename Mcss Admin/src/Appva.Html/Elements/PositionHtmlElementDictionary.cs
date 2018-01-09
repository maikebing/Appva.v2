// <copyright file="PositionHtmlElementDictionary.cs" company="Appva AB">
//     Copyright (c) Appva AB. All rights reserved.
// </copyright>
// <author>
//     <a href="mailto:johan.sall.larsson@appva.com">Johan Säll Larsson</a>
// </author>
namespace Appva.Html.Elements
{
    #region Imports.

    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    /// <summary>
    /// TODO: Add a descriptive summary to increase readability.
    /// </summary>
    internal sealed class PositionHtmlElementDictionary : IDictionary<Position, List<IHtmlElement>>
    {
        #region Variables.

        /// <summary>
        /// The internal dictionary.
        /// </summary>
        private readonly Dictionary<Position, List<IHtmlElement>> dictionary;

        #endregion

        #region Constructors.

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionHtmlElementDictionary"/> class.
        /// </summary>
        public PositionHtmlElementDictionary()
        {
            this.dictionary = new Dictionary<Position, List<IHtmlElement>>();
        }

        #endregion

        #region Properties.

        /// <inheritdoc />
        public int Count
        {
            get
            {
                return this.dictionary.Count();
            }
        }

        /// <inheritdoc />
        public bool IsReadOnly
        {
            get
            {
                return ((ICollection<KeyValuePair<string, object>>) this.dictionary).IsReadOnly;
            }
        }

        /// <inheritdoc />
        public ICollection<Position> Keys
        {
            get
            {
                return this.dictionary.Keys;
            }
        }

        /// <inheritdoc />
        public ICollection<List<IHtmlElement>> Values
        {
            get
            {
                return this.dictionary.Values;
            }
        }

        /// <inheritdoc />
        public List<IHtmlElement> this[Position key]
        {
            get
            {
                List<IHtmlElement> value;
                return this.TryGetValue(key, out value) ? value : new List<IHtmlElement>();
            }

            set
            {
                this.dictionary[key] = value;
            }
        }

        #endregion

        #region Public Members.

        /// <inheritdoc />
        public void Add(Position key, List<IHtmlElement> value)
        {
            this.dictionary.Add(key, value);
        }

        /// <inheritdoc />
        public void Add(KeyValuePair<Position, List<IHtmlElement>> item)
        {
            ((ICollection<KeyValuePair<Position, List<IHtmlElement>>>) this.dictionary).Add(item);
        }

        /// <inheritdoc />
        public void Clear()
        {
            this.dictionary.Clear();
        }

        /// <inheritdoc />
        public bool Contains(KeyValuePair<Position, List<IHtmlElement>> item)
        {
            return ((ICollection<KeyValuePair<Position, List<IHtmlElement>>>) this.dictionary).Contains(item);
        }

        /// <inheritdoc />
        public bool ContainsKey(Position key)
        {
            return this.dictionary.ContainsKey(key);
        }

        /// <inheritdoc />
        public void CopyTo(KeyValuePair<Position, List<IHtmlElement>>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<Position, List<IHtmlElement>>>) this.dictionary).CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public IEnumerator<KeyValuePair<Position, List<IHtmlElement>>> GetEnumerator()
        {
            return ((ICollection<KeyValuePair<Position, List<IHtmlElement>>>) this.dictionary).GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.dictionary.GetEnumerator();
        }

        /// <inheritdoc />
        public bool Remove(Position key)
        {
            return this.dictionary.Remove(key);
        }

        /// <inheritdoc />
        public bool Remove(KeyValuePair<Position, List<IHtmlElement>> item)
        {
            return ((ICollection<KeyValuePair<Position, List<IHtmlElement>>>) this.dictionary).Remove(item);
        }

        /// <inheritdoc />
        public bool TryGetValue(Position key, out List<IHtmlElement> value)
        {
            return this.dictionary.TryGetValue(key, out value);
        }

        #endregion
    }
}