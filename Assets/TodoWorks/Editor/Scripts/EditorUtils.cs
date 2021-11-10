using TodoWorks.Editor.Tests;

using UnityEngine;

namespace TodoWorks.Editor.Utils
{
	public static class EditorUtils
	{
		public static Texture2D Tex(Vector2Int _size, Color _color)
		{
			var tex = new Texture2D(_size.x, _size.y, TextureFormat.RGBA32, false);
			tex.SetPixel(0, 0, new Color(_color.r, _color.g, _color.b));
			tex.Apply();

			return tex;
		}
	}	
}
