using EmawEngineLibrary.Physics;
using Microsoft.Xna.Framework;

namespace EmawEngineLibrary.Collisions
{
    public interface IBoundingObject
    {
        /// <summary>
        /// Checks whether the current BoundingBox intersects another BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check for intersection with.</param>
        bool Intersects(BoundingBox box);

        /// <summary>
        /// Checks whether the current BoundingBox intersects another BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to check for intersection with.</param><param name="result">[OutAttribute] true if the BoundingBox instances intersect; false otherwise.</param>
        void Intersects(ref BoundingBox box, out bool result);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a BoundingFrustum.
        /// </summary>
        /// <param name="frustum">The BoundingFrustum to check for intersection with.</param>
        bool Intersects(BoundingFrustum frustum);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Plane.
        /// </summary>
        /// <param name="plane">The Plane to check for intersection with.</param>
        PlaneIntersectionType Intersects(Plane plane);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Plane.
        /// </summary>
        /// <param name="plane">The Plane to check for intersection with.</param><param name="result">[OutAttribute] An enumeration indicating whether the BoundingBox intersects the Plane.</param>
        void Intersects(ref Plane plane, out PlaneIntersectionType result);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Ray.
        /// </summary>
        /// <param name="ray">The Ray to check for intersection with.</param>
        float? Intersects(Ray ray);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a Ray.
        /// </summary>
        /// <param name="ray">The Ray to check for intersection with.</param><param name="result">[OutAttribute] Distance at which the ray intersects the BoundingBox, or null if there is no intersection.</param>
        void Intersects(ref Ray ray, out float? result);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check for intersection with.</param>
        bool Intersects(BoundingSphere sphere);

        /// <summary>
        /// Checks whether the current BoundingBox intersects a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to check for intersection with.</param><param name="result">[OutAttribute] true if the BoundingBox and BoundingSphere intersect; false otherwise.</param>
        void Intersects(ref BoundingSphere sphere, out bool result);

        /// <summary>
        /// Tests whether the BoundingBox contains another BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to test for overlap.</param>
        ContainmentType Contains(BoundingBox box);

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingBox.
        /// </summary>
        /// <param name="box">The BoundingBox to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        void Contains(ref BoundingBox box, out ContainmentType result);

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingFrustum.
        /// </summary>
        /// <param name="frustum">The BoundingFrustum to test for overlap.</param>
        ContainmentType Contains(BoundingFrustum frustum);

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to test for overlap.</param>
        ContainmentType Contains(BoundingSphere sphere);

        /// <summary>
        /// Tests whether the BoundingBox contains a BoundingSphere.
        /// </summary>
        /// <param name="sphere">The BoundingSphere to test for overlap.</param><param name="result">[OutAttribute] Enumeration indicating the extent of overlap.</param>
        void Contains(ref BoundingSphere sphere, out ContainmentType result);

        /// <summary>
        /// Determine if this box contains, intersects, or is disjoint from the given BoundingBox.
        /// </summary>
        ContainmentType Contains(ref BoundingBox box);

        /// <summary>
        /// Returns true if this box intersects the given other box.
        /// </summary>
        bool Intersects(BoundingOrientedBox other);

        /// <summary>
        /// Determine whether this box contains, intersects, or is disjoint from
        /// the given other box.
        /// </summary>
        ContainmentType Contains(ref BoundingOrientedBox other);
    }
}