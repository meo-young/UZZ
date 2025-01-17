using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

public class LoadTextAssetData : MonoBehaviour
{
    public static LoadTextAssetData instance;

    private void Awake() {
        if(instance == null)
            instance = this;
    }

    public List<T> LoadData<T>(TextAsset textAsset) where T :  new()
    {
        // 클래스의 모든 필드 정보를 가져옴
        FieldInfo[] fields = typeof(T).GetFields();

        // 데이터테이블을 읽을 Reader 선언
        StringReader reader = new(textAsset.text);
        
        // 데이터테이블 첫 줄 건너뛰기
        reader.ReadLine();

        // 데이터 로딩 후 반환할 객체
        List<T> dataList = new List<T>();

        while(reader.Peek() != -1)
        {
            // 리스트에 추가할 데이터
            T dataInstance = new T();

            // 데이터테이블 한 줄 읽어와서 line에 저장
            string[] values = reader.ReadLine().Split('\t');

            for(int i=0; i<values.Length; ++i)
            {
                // 클래스의 멤버변수 타입에 맞게 값 파싱
                object parsedValue = ParseValue(values[i], fields[i].FieldType);

                // 필드에 값 설정
                fields[i].SetValue(dataInstance, parsedValue);
            }
            
            // 리스트에 원소 추가
            dataList.Add(dataInstance);
        }

        return dataList;
    }


    // 멤버변수 타입에 따라 값 파싱해주는 함수
    private object ParseValue(string value, System.Type type)
    {
        if (type == typeof(int))
        {
            return int.Parse(value);
        }
        else if (type == typeof(float))
        {
            return float.Parse(value);
        }
        else if (type == typeof(string))
        {
            return value;
        }

        return null;
    }
}
