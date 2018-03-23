using UnityEngine;

public static class HexMetrics {

	public const float outerRadius = 10f; //六边形到顶点的距离
	public const float innerRadius = outerRadius * 0.866025404f; //六边形到边的距离
	// 关于六个顶点的位置参数
	public static Vector3[] corners = {
		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(0f, 0f, outerRadius)
	};
}