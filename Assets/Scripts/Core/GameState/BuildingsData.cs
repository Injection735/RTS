using Core;
using System.Collections.Generic;

public class BuildingsData
{
	private int _selfBuildingsCount;
	private int _enemyBuildingsCount;

	public void AddSelf() => _selfBuildingsCount++;
	public void AddEnemy() => _enemyBuildingsCount++;

	public void OnSelfBuildingDestroyed() => _selfBuildingsCount--;
	public void OnEnemyBuildingDestroyed() => _enemyBuildingsCount--;

	public bool IsSelfWin() => _enemyBuildingsCount == 0;
	public bool IsEnemyWin() => _selfBuildingsCount == 0;
}
