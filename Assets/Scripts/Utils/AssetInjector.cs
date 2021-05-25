using System.Reflection;

public static class AssetInjector
{
	public static T Inject<T>(this AssetStorage context, T target)
	{
		var targetType = target.GetType();
		var fields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.CreateInstance);

		foreach (var field in fields)
		{
			var injectAssetAttribute = field.GetCustomAttribute(typeof(InjectAssetAttribute)) as InjectAssetAttribute;
			
			if (injectAssetAttribute != null)
			{
				var prefab = context.GetAsset(injectAssetAttribute.AssetName);
				field.SetValue(target, prefab);
			}
		}

		return target;
	}
}
