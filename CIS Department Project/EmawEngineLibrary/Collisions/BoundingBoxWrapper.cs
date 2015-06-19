using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Collisions
{
    public class BoundingBoxWrapper : IBoundingObject
    {
        public BoundingBoxWrapper()
        {
        }

        public BoundingBoxWrapper(BoundingBox boundingBox)
        {
            m_boundingBox = boundingBox;
        }

        public BoundingBoxWrapper(Vector3 min, Vector3 max)
        {
            m_boundingBox = new BoundingBox(min, max);
        }

        public static implicit operator BoundingBox(BoundingBoxWrapper box)
        {
            return box.BoundingBox;
        }

        public static implicit operator BoundingBoxWrapper(BoundingBox box)
        {
            return new BoundingBoxWrapper(box);
        }
        
        public ContainmentType Contains(ref BoundingBox box)
        {
            return box.Contains(m_boundingBox);
        }

        public bool Intersects(BoundingOrientedBox other)
        {
            return other.Intersects(m_boundingBox);
        }

        public ContainmentType Contains(ref BoundingOrientedBox other)
        {
            return other.Contains(ref m_boundingBox);
        }

        public BoundingBox BoundingBox
        {
            get { return m_boundingBox; }
            set { m_boundingBox = value; }
        }

        private BoundingBox m_boundingBox;

        #region Wrapped methods from BoundingBox
        /// <summary>
        /// Gets an array of points that make up the corners of the BoundingBox.
        /// </summary>
        public Vector3[] GetCorners()
        {
            return m_boundingBox.GetCorners();
        }

        /// <summary>
        /// Gets the array of points that make up the corners of the BoundingBox.
        /// </summary>
        /// <param name="corners">An existing array of at least 8 Vector3 points where the corners of the BoundingBox are written.</param>
        public void GetCorners(Vector3[] corners)
        {
            m_boundingBox.GetCorners(corners);
        }

        /// <summary>
        /// Determines whether two instances of BoundingBox are equal.
        /// </summary>
        /// <param name="other">The BoundingBox to compare with the current BoundingBox.</param>
        public bool Equals(BoundingBox other)
        {
            return m_boundingBox.Equals(other);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects another BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check for intersection with.</param>
        public bool Intersects(BoundingBox box)
        {
            return m_boundingBox.Intersects(box);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects another BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check for intersection with.</param><param name="result">[OutAttribute] true if the BoundingBox instances intersect; false otherwise.</param>
        public void Intersects(ref BoundingBox box, out bool result)
        {
            m_boundingBox.Intersects(ref box, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a BoundingFrustum.
        /// </summary>
        /// <param name="frustum">The BoundingFrustum to check for intersection with.</param>
        public bool Intersects(BoundingFrustum frustum)
        {
            return m_boundingBox.Intersects(frustum);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Plane.
        /// </summary>
        /// <param name="plane">The Plane to check for intersection with.</param>
        public PlaneIntersectionType Intersects(Plane plane)
        {
            return m_boundingBox.Intersects(plane);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Plane.
        /// </summary>
        /// <param name="plane">The Plane to check for intersection with.</param><param name="result">[OutAttribute] An enumeration indicating whether the BoundingBox intersects the Plane.</param>
        public void Intersects(ref Plane plane, out PlaneIntersectionType result)
        {
            m_boundingBox.Intersects(ref plane, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Ray.
        /// </summary>
        /// <param name="ray">The Ray to check for intersection with.</param>
        public float? Intersects(Ray ray)
        {
            return m_boundingBox.Intersects(ray);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Ray.
        /// </summary>
        /// <param name="ray">The Ray to check for intersection with.</param><param name="result">[OutAttribute] Distance at which the ray intersects the BoundingBox, or null if there is no intersection.</param>
        public void Intersects(ref Ray ray, out float? result)
        {
            m_boundingBox.Intersects(ref ray, out result);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check for intersection with.</param>
        public bool Intersects(BoundingSphere sphere)
        {
            return m_boundingBox.Intersects(sphere);
        }

        /// <summary>
        /// Checks whether the current BoundingBox intersects a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check for intersection with.</param><param name="result">[OutAttribute] true if the BoundingBox and BoundingSphere intersect; false otherwise.</param>
        public void Intersects(ref BoundingSphere sphere, out bool result)
        {
            m_boundingBox.Intersects(ref sphere, out result);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains another BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to test for overlap.</param>
        public ContainmentType Contains(BoundingBox box)
        {
            return m_boundingBox.Contains(box);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        public void Contains(ref BoundingBox box, out ContainmentType result)
        {
            m_boundingBox.Contains(ref box, out result);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingFrustum.
        /// </summary>
        /// <param name="frustum">The BoundingFrustum to test for overlap.</param>
        public ContainmentType Contains(BoundingFrustum frustum)
        {
            return m_boundingBox.Contains(frustum);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains a point.
        /// </summary>
        /// <param name="point">The point to test for overlap.</param>
        public ContainmentType Contains(Vector3 point)
        {
            return m_boundingBox.Contains(point);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains a point.
        /// </summary>
        /// <param name="point">The point to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        public void Contains(ref Vector3 point, out ContainmentType result)
        {
            m_boundingBox.Contains(ref point, out result);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to test for overlap.</param>
        public ContainmentType Contains(BoundingSphere sphere)
        {
            return m_boundingBox.Contains(sphere);
        }

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        public void Contains(ref BoundingSphere sphere, out ContainmentType result)
        {
            m_boundingBox.Contains(ref sphere, out result);
        }

        #endregion
    }
}
