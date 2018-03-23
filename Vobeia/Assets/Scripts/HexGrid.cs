using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour {
    public int width = 6;
    public int height = 6;
    public HexCell cellPrefab;

    HexCell[] cells;
	
	void CreateCell (int x, int z, int i) {
        //寻找正确的六边形点位
		Vector3 position;
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);

        //Instantiate新的HexCell
		HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        //生成UI坐标和信息注释
        Text label = Instantiate<Text>(cellLabelPrefab);
		label.rectTransform.SetParent(gridCanvas.transform, false);
		label.rectTransform.anchoredPosition = new Vector2(position.x, position.z);
		label.text = cell.coordinates.ToStringOnSeparateLines();

        cell.color = defaultColor;
	}
/*
	public void TouchCell (Vector3 position) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		Debug.Log("touched at " + coordinates.ToString());
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		hexMesh.Triangulate(cells);
	}
*/
    public void ColorCell (Vector3 position, Color color) {
		position = transform.InverseTransformPoint(position);
		HexCoordinates coordinates = HexCoordinates.FromPosition(position);
		int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
		HexCell cell = cells[index];
		cell.color = color;
		hexMesh.Triangulate(cells);
	}

	public Color defaultColor = Color.white;


    void Start() {
        hexMesh.Triangulate(cells);
    }

    public Text cellLabelPrefab; //标注cell的坐标和信息
	Canvas gridCanvas; //放置Text UI
    HexMesh hexMesh;

    void Awake () {
        gridCanvas = GetComponentInChildren<Canvas>();
		hexMesh = GetComponentInChildren<HexMesh>();
        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++) {
            for (int x = 0; x < width; x++) {
                CreateCell(x, z, i++);
            }
        }
	}
}