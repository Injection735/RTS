using System.Reflection;

public static class AssetInjector
{
	public static T Inject<T>(this AssetStorage context, T target)
	{
		var selectedTargetType = target.GetType();

		while (selectedTargetType != typeof(object))
		{
			var fields = selectedTargetType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

			foreach (var field in fields)
			{
				var injectAssetAttribute = field.GetCustomAttribute(typeof(InjectAssetAttribute)) as InjectAssetAttribute;
			
				if (injectAssetAttribute != null)
				{
					var prefab = context.GetAsset(injectAssetAttribute.AssetName);
					field.SetValue(target, prefab);
				}
			}

			selectedTargetType = selectedTargetType.BaseType;
		}

		return target;
	}
}
