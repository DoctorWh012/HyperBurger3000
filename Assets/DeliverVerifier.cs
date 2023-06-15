using UnityEngine;

public class DeliverVerifier : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private ParticleSystem correctDeliverParticles;
    [SerializeField] private ParticleSystem wrongDeliverParticles;

    public Ingredients[] requestedRecipe;
    private GameObject deliveredPlate;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Plate")) return;
        Plate plate = other.GetComponent<Plate>();
        deliveredPlate = other.gameObject;
        if (plate.stackedIngredientsQnt != requestedRecipe.Length)
        {
            FailRecipeDeliver();
            return;
        }

        for (int i = 0; i < requestedRecipe.Length; i++)
        {
            if (requestedRecipe[i] != plate.stackedIngredients[i])
            {
                FailRecipeDeliver();
                return;
            }
        }
        AcceptRecipeDeliver();
    }

    private void FailRecipeDeliver()
    {
        print("WrongRecipe");
        wrongDeliverParticles.Play();
        GameManager.Instance.DeliveredWrong();
        deliveredPlate = null;
    }

    private void AcceptRecipeDeliver()
    {
        print("CorrectRecipe");
        correctDeliverParticles.Play();
        GameManager.Instance.DeliveredCorrect();
        Destroy(deliveredPlate);
    }
}
