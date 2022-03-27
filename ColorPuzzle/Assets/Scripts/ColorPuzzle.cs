using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPuzzle : MonoBehaviour
{
    const int MAX_ROW_COUNT = 5;
    const int MAX_COL_COUNT = 5;
    const int MAX_BLCOCK_COUNT = MAX_ROW_COUNT * MAX_COL_COUNT;

    const int HOLE_TARGET_INDEX = 13;

    public ColorBlock ref_colorBlock;

    ColorBlock[,] blocks = new ColorBlock[MAX_ROW_COUNT,MAX_COL_COUNT];

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    void Init()
    {

        //for (int y = 3; y >= 0; y--)
        //{
        //    for (int x = 0; x < 4; x++)
        //    {
        //        ColorBlock block = Instantiate(ref_colorBlock, new Vector2(x, y), Quaternion.identity, transform);
        //        block.Init(x, y, n + 1, sprites_number[n], ClickToSwap);
        //        blocks[x, y] = block;
        //        n++;
        //    }
        //}

        int n = 1;
        bool isHoleCheck = false;
        int holeTargetIndex = HOLE_TARGET_INDEX;
        if (HOLE_TARGET_INDEX > MAX_BLCOCK_COUNT)
        {
            holeTargetIndex = MAX_BLCOCK_COUNT;
            Debug.LogError("Target Over Count");
        }

        for (int y = 0; y < MAX_COL_COUNT; y++)
        {
            for (int x = 0; x < MAX_ROW_COUNT; x++)
            {
                ColorBlock block = Instantiate(ref_colorBlock, new Vector2(x, y), Quaternion.identity, transform);

                if (n == holeTargetIndex)
                {
                    block.Init(x, y, 0, ClickToSwap);
                    isHoleCheck = true;

                }
                else
                {
                    int addedCount = 0;
                    if (isHoleCheck) //홀이 중간에 삽입시 해당 자리에 추가 1 인덱스가 발생
                    {
                        addedCount = 1;
                    }
                    block.Init(x, y, n - addedCount, ClickToSwap); //인덱스를 빼주어 값을 보정
                    
                }
                blocks[x, y] = block;
                n++;
            }
        }
    }

    void ClickToSwap(int x, int y)
    {
        int dx = getDx(x, y);
        int dy = getDy(x, y);

        var from = blocks[x, y];
        var target = blocks[x + dx, y + dy];

        blocks[x, y] = target;
        blocks[x + dx, y + dy] = from;

        //from.UpdatePos(x+dx, y +dy);
        //target.UpdatePos(x, y);

        //if (dx >= 0)
        //{
        //    for (int i = dx; i == x; i--)
        //    {
        //        var froms = blocks[x + i, y];
        //        froms.MovePos(i + 1, y);
        //    }
        //}
        //else
        //{
        //    for (int i = 0; i == dx; i++)
        //    {
        //        var froms = blocks[i, y];
        //        froms.MovePos(i-1 ,y);
        //    }
        //}


        from.MovePos(x + dx, y + dy);
        target.MovePos(x, y);
    }

    int getDx(int x, int y)
    {
        // is right empty
        //if (x < MAX_ROW_COUNT-1 && blocks[x + 1, y].IsEmpty())
        //    return 1;

        int dirAmount = 0;
        for (int i = x; i < MAX_ROW_COUNT; i++)
        {
            if (blocks[i,y].IsEmpty())
            {
                return dirAmount;
            }
            dirAmount++;
        }

        //// is left empty
        //if (x > 0 && blocks[x - 1, y].IsEmpty())
        //    return -1;
        dirAmount = 0;
        for (int i = x; i >= 0; i--)
        {
            if (blocks[i, y].IsEmpty())
            {
                return dirAmount;
            }
            dirAmount--;
        }


        return 0;
    }

    int getDy(int x, int y)
    {
        // is Bottom empty
        //if (x < MAX_ROW_COUNT-1 && blocks[x + 1, y].IsEmpty())
        //    return 1;

        int dirAmount = 0;
        for (int i = y; i < MAX_COL_COUNT; i++)
        {
            if (blocks[x, i].IsEmpty())
            {
                return dirAmount;
            }
            dirAmount++;
        }

        //// is Top empty
        //if (x > 0 && blocks[x - 1, y].IsEmpty())
        //    return -1;
        dirAmount = 0;
        for (int i = y; i >= 0; i--)
        {
            if (blocks[x, i].IsEmpty())
            {
                return dirAmount;
            }
            dirAmount--;
        }
        return 0;
    }
    
}
