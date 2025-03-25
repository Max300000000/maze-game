using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generation : MonoBehaviour
{
    [SerializeField] GameObject whiteTile;
    [SerializeField] GameObject blackTile;
    [SerializeField] GameObject key;

    [SerializeField] GameObject wall1;
    [SerializeField] GameObject wall2;
    [SerializeField] GameObject wall3;
    [SerializeField] GameObject wall4;

    [SerializeField] GameObject wall5;
    [SerializeField] GameObject wall6;
    [SerializeField] GameObject wall7;
    [SerializeField] GameObject wall8;

    [SerializeField] GameObject wall52;
    [SerializeField] GameObject wall62;
    [SerializeField] GameObject wall72;
    [SerializeField] GameObject wall82;

    GameObject player;

    public GameObject child1;   //white
    public GameObject child2;   //black
    public GameObject child3;   //keys
    public GameObject child4;   //keycheck

    public CharacterData cd;
    
    [SerializeField] int mapSize = 25;
    int roomSize = 3;
    int walkers = 4;     
    [SerializeField] int dupChance = 10;
    int maxWalkers = 8;
    [SerializeField] int breakChance = 5;
    int keys = 3;

    Grid[,] gridArray;

    public enum Grid{
        WALL,
        OPEN,
        COLLAPSED
    }
    
    public class Walker{
        int posX;
        int posY;
        List<int> memoryX = new List<int>();
        List<int> memoryY = new List<int>();

        public Walker(int x, int y){
            posX = x;
            posY = y;
        }

        public int PosX { get { return posX; } set { posX = value; } }
        public int PosY { get { return posY; } set { posY = value; } }
        public int memory { get {return memoryX.Count; } }
        public int LastX { get { return memoryX[memoryX.Count -1]; } }
        public int LastY { get { return memoryY[memoryY.Count -1]; } }

        public void AddMemory(int x, int y){
            memoryX.Add(x);
            memoryY.Add(y);
        }

        public void RemoveMemory(){
            if (memoryX.Count > 0 && memoryY.Count > 0){
            memoryX.RemoveAt(memoryX.Count -1);
            memoryY.RemoveAt(memoryY.Count -1);}}
    }

    public List<Walker> allWalkers = new List<Walker>();
    bool generated = false;



    void Start(){
        player = GameObject.Find("Player");
        GenerateMaze();
    }

    void GenerateMaze(){
        MakeGrid();
        MakeRoom();
        MovePlayer();
        InitializeWalkers();
        LetThemWalk();

        MakeTiles();
        MakeKeys();
        generated = true;
    }

    void MakeGrid(){    //udda = squares
        if (!generated){
            gridArray = new Grid[mapSize * 2 + 1, mapSize * 2 + 1];}
        for (int x = 0; x < mapSize * 2 + 1; x ++){
            for(int y = 0; y < mapSize * 2 + 1; y ++){
                if (x % 2 == 0 || y % 2 == 0){
                    gridArray[x, y] = Grid.WALL;}
                else{
                    gridArray[x, y] = Grid.OPEN;}}}
    }

    void MakeRoom(){
        for (int x = 0; x < roomSize * 2 - 1; x ++){
            for(int y = 0; y < roomSize * 2 - 1; y ++){
                gridArray[x + mapSize - roomSize + 1, y + mapSize - roomSize + 1] = Grid.COLLAPSED;}
        }
        List<int> xP = new List<int> { mapSize, mapSize + roomSize, mapSize, mapSize - roomSize };
        List<int> yP = new List<int> { mapSize + roomSize, mapSize, mapSize - roomSize, mapSize };
        for (int z = 0; z < 4; z ++){
            gridArray[xP[z], yP[z]] = Grid.COLLAPSED;}
    }

    void MovePlayer(){
        player.transform.position = new Vector3(mapSize, mapSize, 0);
        child4.transform.position = new Vector3(mapSize, mapSize, 0);
    }

    void InitializeWalkers(){
        while (allWalkers.Count > 0){
            allWalkers.RemoveAt(0);}
        List<int> xP = new List<int> { mapSize, mapSize + roomSize + 1, mapSize, mapSize - roomSize - 1 };
        List<int> yP = new List<int> { mapSize + roomSize + 1, mapSize, mapSize - roomSize - 1, mapSize };
        List<int> index = new List<int> { 0, 1, 2, 3 };
        int madeWalkers = 0;
        while (madeWalkers < walkers){
            int randomIndex = index[Random.Range(0, index.Count)];
            index.Remove(randomIndex);
            MakeWalker(xP[randomIndex], yP[randomIndex]);
            gridArray[xP[randomIndex], yP[randomIndex]] = Grid.COLLAPSED;
            madeWalkers ++;
            if (index.Count == 0){
                index = new List<int> { 0, 1, 2, 3 };}
        }
        for (int x = 0; x < allWalkers.Count; x++){
            gridArray[allWalkers[x].PosX, allWalkers[x].PosY] = Grid.COLLAPSED;}
    }

    void MakeWalker(int x, int y){
        Walker walker = new Walker(x, y);
        allWalkers.Add(walker);
    }

    void LetThemWalk(){
        int x = 0;
        int b = 0;
        while (true){
            List<Walker> willWalk = new List<Walker>(allWalkers);
            while (willWalk.Count > 0){
                Walker walker = willWalk[Random.Range(0, willWalk.Count)];
                willWalk.Remove(walker);
                WalkAttempt(walker, ref b);}
            x ++;
            if ((x >= 300) && (x % 20 == 0)){
                if (MazeIsDone()){return;}
                if (x >= 600){Debug.Log("Incomplete"); return;}}}
    }
    
    void WalkAttempt(Walker walker, ref int breaks){
        List<int> xD = new List<int> { 0, 2, 0, -2 };
        List<int> yD = new List<int> { 2, 0, -2, 0 };
        List<int> directions = new List<int> { 0, 1, 2, 3 };
        int tries = 0;
        while(tries < 4){
            int direction = directions[Random.Range(0, directions.Count)];
            directions.Remove(direction);
            if (walker.PosX + xD[direction] < 0 || walker.PosX + xD[direction] > mapSize * 2 ||
                walker.PosY + yD[direction] < 0 || walker.PosY + yD[direction] > mapSize * 2) {tries ++; continue;
            }
            if (gridArray[walker.PosX + xD[direction], walker.PosY + yD[direction]] != Grid.COLLAPSED){
                Move(walker, direction); return;
            }
            else {
                int randomN = Random.Range(1, 101);
                if (breakChance >= randomN){
                    BackAndForth(walker, direction); breaks ++; return;}
            }
            tries ++;
        }
        Backtrack(walker);
    }
    
    void Move(Walker walker, int direction){
        List<int> xD = new List<int> { 0, 2, 0, -2 };
        List<int> yD = new List<int> { 2, 0, -2, 0 };
        walker.AddMemory(walker.PosX, walker.PosY);
        walker.PosX = walker.LastX + xD[direction];
        walker.PosY = walker.LastY + yD[direction]; 
        gridArray[walker.LastX + xD[direction] / 2, walker.LastY + yD[direction] / 2] = Grid.COLLAPSED;
        gridArray[walker.PosX, walker.PosY] = Grid.COLLAPSED;
        Duplicate(walker);
    }

    void BackAndForth(Walker walker, int direction){
        List<int> xD = new List<int> { 0, 2, 0, -2 };
        List<int> yD = new List<int> { 2, 0, -2, 0 };
        gridArray[walker.PosX + xD[direction] / 2, walker.PosY + yD[direction] / 2] = Grid.COLLAPSED;
        gridArray[walker.PosX + xD[direction], walker.PosY + yD[direction]] = Grid.COLLAPSED;
    }

    void Backtrack(Walker walker){
        if (walker.memory == 0) {allWalkers.Remove(walker); return;}
        walker.PosX = walker.LastX;
        walker.PosY = walker.LastY;
        walker.RemoveMemory();
    }

    void Duplicate(Walker walker){
        if (allWalkers.Count < maxWalkers){
            int randomN = Random.Range(1, 101);
            if (dupChance >= randomN){
                MakeWalker(walker.PosX, walker.PosY);}}
    }

    bool MazeIsDone(){ 
        for (int x = 1; x < mapSize * 2 + 1; x += 2){
            for(int y = 1; y < mapSize * 2 + 1; y += 2){
                if (gridArray[x, y] != Grid.COLLAPSED){
                    return false;}}
        }
        return true;
    }

    void MakeTiles(){
        if (generated){
            foreach (Transform grandchild in child2.transform){
                Destroy(grandchild.gameObject);}
            foreach (Transform grandchild in child1.transform){
                Destroy(grandchild.gameObject);}
        }
        else {
            for (int x = 0; x < mapSize * 2 + 1; x ++){
                for(int y = 0; y < mapSize * 2 + 1; y ++){
                    MakeTile(x, y);}}
        }
        for (int x = 0; x < mapSize * 2 + 1; x ++){
            for(int y = 0; y < mapSize * 2 + 1; y ++){
                if (gridArray[x, y] == Grid.WALL){
                    if (cd.u3) {MakeWall2(x, y);}
                    else {MakeWall(x, y);}}}}
    }

    void MakeTile(int x, int y){
        GameObject tile;
        if (cd.u3) {tile = blackTile;}
        else {tile = whiteTile;}
        GameObject newTile = Instantiate(tile, new Vector3(x, y, 0), Quaternion.identity);
        newTile.transform.SetParent(child1.transform);
        newTile.name = x + ". " + y;
    }

    void MakeWallTile(GameObject wall, int x, int y){
        GameObject newTile = Instantiate(wall, new Vector3(x, y, 0), Quaternion.identity);
        newTile.transform.SetParent(child2.transform);
        newTile.name = x + ". " + y;
    }
        
    void MakeWall(int x, int y){
        List<GameObject> walls = new List<GameObject> { wall1, wall2, wall3, wall4 };
        List<bool> neighbors = NeighborCheck(x, y);
        if (!neighbors[0]) {walls.Remove(wall1);}
        if (!neighbors[1]) {walls.Remove(wall2);}
        if (!neighbors[2]) {walls.Remove(wall3);}
        if (!neighbors[3]) {walls.Remove(wall4);}
        foreach (GameObject wall in walls){
            MakeWallTile(wall, x, y);} 
    }

    void MakeWall2(int x, int y){
        List<GameObject> walls = new List<GameObject> { wall5, wall6, wall7, wall8, wall52, wall62, wall72, wall82 };
        List<bool> neighbors = NeighborCheck(x, y);
        if (!neighbors[0]) {walls.Remove(wall5); walls.Remove(wall52); walls.Remove(wall7);}
        if (!neighbors[1]) {walls.Remove(wall6); walls.Remove(wall62); walls.Remove(wall8);}
        if (!neighbors[2]) {walls.Remove(wall7); walls.Remove(wall72); walls.Remove(wall5);}
        if (!neighbors[3]) {walls.Remove(wall8); walls.Remove(wall82); walls.Remove(wall6);}
        foreach (GameObject wall in walls){
            MakeWallTile(wall, x, y);} 
    }

    void MakeKey(int x, int y){
        GameObject newKey = Instantiate(key, new Vector3(x, y, 0), Quaternion.identity);
        newKey.transform.SetParent(child3.transform);
        newKey.name = "Key "; 
    }

    List<bool> NeighborCheck(int x, int y){
        List<bool> neighbors = new List<bool>();
        List<int> xD = new List<int> { 0, 1, 0, -1 };
        List<int> yD = new List<int> { 1, 0, -1, 0 };
        for (int z = 0; z < 4; z ++){
            if (x + xD[z] < 0 || x + xD[z] > mapSize * 2) {neighbors.Add(false); continue; } 
            if (y + yD[z] < 0 || y + yD[z] > mapSize * 2) {neighbors.Add(false); continue; } 
            neighbors.Add(gridArray[x + xD[z], y + yD[z]] == Grid.WALL);
        }
        return neighbors;
    }

    void MakeKeys(){
        if (generated){
            foreach (Transform grandchild in child3.transform){
                Destroy(grandchild.gameObject);}
        }  
        List<int> min = new List<int> { mapSize + roomSize + 2, mapSize + roomSize + 2, 2, 2 };
        List<int> max = new List<int> { mapSize * 2, mapSize * 2, mapSize - 3, mapSize - 3 };
        int keysMade = 0;
        while (keysMade < keys){
            int rI = Random.Range(0, min.Count);
            int randomX = Random.Range(min[rI], max[rI]);
            int randomY = Random.Range(min[rI], max[rI]);
            if (gridArray[randomX, randomY] != Grid.WALL){
                MakeKey(randomX, randomY);
                keysMade ++;
                min.RemoveAt(rI);
                max.RemoveAt(rI);
            }
        }
    }
        



}
