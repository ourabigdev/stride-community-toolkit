using Stride.BepuPhysics.Definitions.Colliders;
using Stride.Core.Mathematics;

namespace Stride.CommunityToolkit.Bepu.Colliders;

/// <summary>
/// Provides helpers to construct a Bepu <see cref="BoxCollider"/> for a quad (flat rectangle) primitive.
/// </summary>
public static class QuadCollider
{
    /// <summary>
    /// Creates a <see cref="BoxCollider"/> sized to match a quad mesh.
    /// </summary>
    /// <param name="size">Width (X) and height (Y) of the quad. When null, defaults from <see cref="QuadProceduralModel"/> are used.</param>
    /// <param name="depth">Thickness along Z. A small positive value keeps the collider valid in 3D.</param>
    public static BoxCollider Create(Vector2? size = null, float depth = 1f)
    {
        var validSize = size ?? Vector2.One; // matches QuadProceduralModel default

        return new BoxCollider { Size = new Vector3(validSize.X, validSize.Y, depth) };
    }
}