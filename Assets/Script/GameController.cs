using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int level;
    public int row, col, countStep;
    public int rowBlank, colBlank;// position of image blank
    public int sizeRow, sizeCol;
    public int countPoint = 0;
    public int countImageKey = 0;
    public bool startControl = false;
    public bool checkComplete;
    GameObject temp;
    public List<GameObject> imageKeyList;// run from 0 ------> list.count
    public List<GameObject> imageOfPictureList;
    public List<GameObject> checkPointList;

    GameObject[,] imageKeyMatrix;
    GameObject[,] imageOfPictureMatrix;
    GameObject[,] checkPointMatrix;

    // Start is called before the first frame update
    void Start()
    {
        imageKeyMatrix = new GameObject[sizeRow,sizeCol];
        imageOfPictureMatrix = new GameObject[sizeRow,sizeCol];
        checkPointMatrix = new GameObject[sizeRow,sizeCol];
        if (level==1)
        {
            ImageOfEasyLevel();
        } else if (level==2){
            ImageOfNormalLevel();
        } else if (level==3){
            ImageOfHardLevel();
        }
        // Call function
        CheckPointManager();
        imageKeyManager();
        for (int r = 0; r < sizeRow; r++)//run row
        {
            for (int c = 0; c < sizeRow; c++)// run column
            {
                if(imageOfPictureMatrix[r, c].name.CompareTo("blank")==0)
                {
                    rowBlank=r;
                    colBlank=c;
                    break;
                }
                countPoint++;
            }
        }
    }

    void CheckPointManager()
    {
        for (int r = 0; r < sizeRow; r++)//run row
        {
            for (int c = 0; c < sizeRow; c++)// run column
            {
                checkPointMatrix[r, c] = checkPointList[countPoint];
                countPoint++;
            }
        }
    }
    void imageKeyManager()
    {
        for (int r = 0; r < sizeRow; r++)//run row
        {
            for (int c = 0; c < sizeRow; c++)// run column
            {
                imageKeyMatrix[r, c] = imageKeyList[countImageKey];
                countImageKey++;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {   
        if (startControl)
        { // move for image of picture
            startControl = false;
            if (countStep == 1)
            {
                if (imageOfPictureMatrix[row, col]!=null&&imageOfPictureMatrix[row, col].name.CompareTo("blank") != 0)//check if position touch is image not image blank
                {
                    if (rowBlank!=row&&colBlank!=col)
                    {   //move
                        SortImage();//call function ImageSort
                        // countStep=0;// reset countstep
                    } else if (rowBlank==row && colBlank==col){
                        //move
                        //SortImage();//call function ImageSort
                        // countStep=0;
                    }else if ((rowBlank==row && colBlank==col)|| (rowBlank!=row && colBlank!=col)){
                        countStep=0;// not move
                    }
                } else {
                    countStep=0;//not move
                }
            }
        }
    }
    void SortImage(){ // sort
        temp = imageOfPictureMatrix[rowBlank, colBlank];//select image blanak and save at temp variable
        imageOfPictureMatrix[rowBlank, colBlank] =null;
        imageOfPictureMatrix[rowBlank, colBlank] = imageOfPictureMatrix[row, col];// select image is no image blank and and save it at new position
        imageOfPictureMatrix[row, col]=null;
        imageOfPictureMatrix[row, col]=temp;

        // set move for image;
        imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().target=checkPointMatrix[rowBlank, colBlank]; // set new point for image blank
        imageOfPictureMatrix[row, col].GetComponent<ImageController>().target=checkPointMatrix[row, col];

        imageOfPictureMatrix[rowBlank, colBlank].GetComponent<ImageController>().startMove = true;
        imageOfPictureMatrix[row, col].GetComponent<ImageController>().startMove = true;

        // set row and col for image blank

        rowBlank = row;// position touch;
        colBlank = col;
    }
    void ImageOfEasyLevel()
    {    //9 row
        imageOfPictureMatrix[0, 0] = imageOfPictureList[0];
        imageOfPictureMatrix[0, 1] = imageOfPictureList[2];
        imageOfPictureMatrix[0, 2] = imageOfPictureList[5];
        imageOfPictureMatrix[1, 0] = imageOfPictureList[4];
        imageOfPictureMatrix[1, 1] = imageOfPictureList[1];
        imageOfPictureMatrix[1, 2] = imageOfPictureList[7];
        imageOfPictureMatrix[2, 0] = imageOfPictureList[3];
        imageOfPictureMatrix[2, 1] = imageOfPictureList[6];
        imageOfPictureMatrix[2, 2] = imageOfPictureList[8];
    }
    void ImageOfNormalLevel()
    {    //16 row
        imageOfPictureMatrix[0, 0] = imageOfPictureList[4];
        imageOfPictureMatrix[0, 1] = imageOfPictureList[0];
        imageOfPictureMatrix[0, 2] = imageOfPictureList[1];
        imageOfPictureMatrix[0, 3] = imageOfPictureList[2];
        imageOfPictureMatrix[1, 0] = imageOfPictureList[8];
        imageOfPictureMatrix[1, 1] = imageOfPictureList[6];
        imageOfPictureMatrix[1, 2] = imageOfPictureList[7];
        imageOfPictureMatrix[1, 3] = imageOfPictureList[11];
        imageOfPictureMatrix[2, 0] = imageOfPictureList[12];
        imageOfPictureMatrix[2, 1] = imageOfPictureList[5];
        imageOfPictureMatrix[2, 2] = imageOfPictureList[14];
        imageOfPictureMatrix[2, 3] = imageOfPictureList[10];
        imageOfPictureMatrix[3, 0] = imageOfPictureList[13];
        imageOfPictureMatrix[3, 1] = imageOfPictureList[9];
        imageOfPictureMatrix[3, 2] = imageOfPictureList[15];
        imageOfPictureMatrix[3, 3] = imageOfPictureList[3];// image is 4 and at position 3 in the list

    }
    void ImageOfHardLevel()
    {    //25 row
        imageOfPictureMatrix[0, 0] = imageOfPictureList[5];//row 1
        imageOfPictureMatrix[0, 1] = imageOfPictureList[2];
        imageOfPictureMatrix[0, 2] = imageOfPictureList[3];
        imageOfPictureMatrix[0, 3] = imageOfPictureList[4];
        imageOfPictureMatrix[0, 4] = imageOfPictureList[9];
        imageOfPictureMatrix[1, 0] = imageOfPictureList[10];//row 2
        imageOfPictureMatrix[1, 1] = imageOfPictureList[1];
        imageOfPictureMatrix[1, 2] = imageOfPictureList[12];
        imageOfPictureMatrix[1, 3] = imageOfPictureList[7];
        imageOfPictureMatrix[1, 4] = imageOfPictureList[8];
        imageOfPictureMatrix[2, 0] = imageOfPictureList[15];//row 3
        imageOfPictureMatrix[2, 1] = imageOfPictureList[6];
        imageOfPictureMatrix[2, 2] = imageOfPictureList[13];
        imageOfPictureMatrix[2, 3] = imageOfPictureList[14];
        imageOfPictureMatrix[2, 4] = imageOfPictureList[19];
        imageOfPictureMatrix[3, 0] = imageOfPictureList[20];//row 4
        imageOfPictureMatrix[3, 1] = imageOfPictureList[11];
        imageOfPictureMatrix[3, 2] = imageOfPictureList[22];
        imageOfPictureMatrix[3, 3] = imageOfPictureList[17];
        imageOfPictureMatrix[3, 4] = imageOfPictureList[18];
        imageOfPictureMatrix[4, 0] = imageOfPictureList[21];//row 5
        imageOfPictureMatrix[4, 1] = imageOfPictureList[16];
        imageOfPictureMatrix[4, 2] = imageOfPictureList[23];
        imageOfPictureMatrix[4, 3] = imageOfPictureList[24];
        imageOfPictureMatrix[4, 4] = imageOfPictureList[0]; // image is blank is number is and at position 0 in list
    }
}
