using UnityEngine;

public class PlayerColision : MonoBehaviour
{
    Player player;
    bool collideFire = false;
    SoundManager soundManager;
    private void Start()
    {
        player = GetComponent<Player>();        
        soundManager = GetComponent<SoundManager>();
    }
   private void OnCollisionEnter2D(Collision2D other)
   {

        player.SetColorSpr(Color.white);

        switch (other.gameObject.tag){
            case "Fire":
                player.ForceExitIce();
                soundManager.PlayAudio("Fire");

                if (collideFire)
                    player.Dead(false);
                else{
                    player.Fire();
                    collideFire = true;
                }
                break;
            case "Spike":
                player.Dead(false);
                break;
            case "Spring":
                player.ForceExitIce();
                other.gameObject.GetComponent<Animator>().SetTrigger("Jump");
                player.SetJumpForce();
                collideFire = false;
                soundManager.PlayAudio("Spring");
                break;
            case "Ice":
                collideFire = false;
                player.SetAccereration(false);
                break;
            case "Fall":
                soundManager.PlayAudio("Fall");
                player.Dead(true);
                break;
            default:
                collideFire = false;
                player.ForceExitIce();
                break;
        }

   }
    
   private void OnCollisionExit2D(Collision2D other)
   {
       switch(other.gameObject.tag){
            case "Ice":
                player.Invoke("ExitIce", 1f);
                break;
        }
   }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "End":
                player.EndLevel();
                break;
        }
    }
}
