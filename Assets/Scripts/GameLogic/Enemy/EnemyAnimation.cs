using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public List<EnemyAnim> anims;

    public string GetAnimName(string name)
    {
        return anims.Find(x => x.name == name).animName;
    }

}
[System.Serializable]
public class EnemyAnim
{
    public string name;
    public string animName;
}