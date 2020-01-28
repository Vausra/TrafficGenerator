// <copyright file="SerialComElements.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

/// <summary>
/// Namespace of the serial port traffic generator
/// </summary>
namespace SerialCommInterface
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Data container object.
    /// </summary>
    public class SerialComElements : IEquatable<SerialComElements>
    {
        /// <summary>
        /// Gets or sets the MessageName of a request/response.
        /// </summary>
        public string MessageName { get; set; }

        /// <summary>
        /// Gets or sets the MessageResponse.
        /// </summary>
        public string MessageResponse { get; set; }

        /// <summary>
        /// Gets or sets the MessageRequest.
        /// </summary>
        public string MessageRequest { get; set; }

        /// <summary>
        /// Equality comparator for <see cref="SerialComElements"/>.
        /// </summary>
        /// <param name="left">Object on the left of the euqality comparator</param>
        /// <param name="right">Object on the right of the euqality comparator</param>
        /// <returns>True if the elements are equal otherwise false.</returns>
        public static bool operator ==(SerialComElements left, SerialComElements right)
        {
            return EqualityComparer<SerialComElements>.Default.Equals(left, right);
        }

        /// <summary>
        /// Unequality comparator for <see cref="SerialComElements"/>.
        /// </summary>
        /// <param name="left">Object on the left of the euqality comparator.</param>
        /// <param name="right">Object on the right of the euqality comparator.</param>
        /// <returns>True if the elements are equal otherwise false.</returns>
        public static bool operator !=(SerialComElements left, SerialComElements right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Equals method for <see cref="object"/>.
        /// </summary>
        /// <param name="obj">Object to check equality for.</param>
        /// <returns>True if objects are equal otherwise false</returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as SerialComElements);
        }

        /// <summary>
        /// Equals method for <see cref="SerialComElements"/>.
        /// </summary>
        /// <param name="serialComElement">Object to check equality for (null is allowed).</param>
        /// <returns>True if objects are equal otherwise false.</returns>
        public bool Equals([AllowNull] SerialComElements serialComElement)
        {
            return serialComElement != null &&
                   this.MessageName == serialComElement.MessageName &&
                   this.MessageResponse == serialComElement.MessageResponse &&
                   this.MessageRequest == serialComElement.MessageRequest;
        }

        /// <summary>
        /// GetHashCode method for <see cref="SerialComElements"/>.
        /// </summary>
        /// <returns>Calculated HashCode.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(this.MessageName, this.MessageResponse, this.MessageRequest);
        }
    }
}
