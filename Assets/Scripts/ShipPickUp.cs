using UnityEngine;

public class ShipPickUp : MonoBehaviour
{
    public GameObject shiptype;
    public Sprite shipSprite;

    public void pickUp()
    {
        Assimilation.ShipData ship = new Assimilation.ShipData();
        ship.shipType = shiptype;
        ship.stickerSprite = shipSprite;
        Assimilation.collectionList.Add(ship);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUp();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.CompareTag("BulletWall"))
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("minion"))
        {
            pickUp();
            Destroy(this.gameObject);
        }
    }
}
