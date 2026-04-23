using Stride.Graphics;
using Stride.Rendering.ProceduralModels;

namespace Stride.CommunityToolkit.Rendering.ProceduralModels;

/// <summary>
/// Procedural model that generates a flat quad (rectangle) in the XY plane.
/// </summary>
public class QuadProceduralModel : PrimitiveProceduralModelBase
{
    /// <summary>Width and height of the quad in local space.</summary>
    public Vector2 Size { get; set; } = Vector2.One;

    private static readonly Dictionary<MeshCacheKey, GeometricMeshData<VertexPositionNormalTexture>> _meshCache = [];
    private readonly record struct MeshCacheKey(Vector2 Size, float UScale, float VScale, bool ToLeftHanded);

    /// <inheritdoc />
    protected override GeometricMeshData<VertexPositionNormalTexture> CreatePrimitiveMeshData()
        => New(Size, UvScale.X, UvScale.Y);

    /// <summary>Creates (or retrieves from cache) a quad mesh.</summary>
    public static GeometricMeshData<VertexPositionNormalTexture> New(Vector2 size, float uScale = 1f, float vScale = 1f, bool toLeftHanded = false)
    {
        var key = new MeshCacheKey(size, uScale, vScale, toLeftHanded);
        if (_meshCache.TryGetValue(key, out var mesh)) return mesh;
        mesh = CreateMesh(size, uScale, vScale, toLeftHanded);
        _meshCache[key] = mesh;
        return mesh;
    }

    /// <summary>Builds a new quad mesh (no caching).</summary>
    public static GeometricMeshData<VertexPositionNormalTexture> CreateMesh(Vector2 size, float uScale = 1f, float vScale = 1f, bool toLeftHanded = false)
    {
        var half = size / 2f;
        var uvScale = new Vector2(uScale, vScale);
        var normal = Vector3.UnitZ;

        var vertices = new VertexPositionNormalTexture[4];
        vertices[0] = new(new Vector3(-half.X, -half.Y, 0), normal, new Vector2(0, 0) * uvScale);
        vertices[1] = new(new Vector3( half.X, -half.Y, 0), normal, new Vector2(1, 0) * uvScale);
        vertices[2] = new(new Vector3( half.X,  half.Y, 0), normal, new Vector2(1, 1) * uvScale);
        vertices[3] = new(new Vector3(-half.X,  half.Y, 0), normal, new Vector2(0, 1) * uvScale);

        var indices = new int[] { 0, 2, 1, 0, 3, 2 };

        return new GeometricMeshData<VertexPositionNormalTexture>(vertices, indices, toLeftHanded) { Name = "Quad" };
    }
}