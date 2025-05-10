using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;
    [SerializeField] private ParticleSystem destroyParticles;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.Instance.PlaySFX(destroySound);
        Instantiate(destroyParticles, transform.position, Quaternion.identity);
        ApplesManager.Instance.AddApples(1);
        Destroy(this.gameObject);
    }
}    
 
