using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public void LoadScene(int level)
	{
		// level on ladattavan scenen indeksi
		// Lisää itemi Buttonin OnClick()-listaan +-painikkeesta
		// Dräggää & droppaa tämä skripti UI-objektiin ja sitten dräggää UI-objekti OnClick()-listaan.
		// Valitse funktio MenuManager > LoadScene(int)
		// Build Settings... > Scenes In Build: dräggää & droppaa ensin menu-scene ja sitten level01 niin, että menu on ylin. Näin menu (indeksi 0) latautuu, kun peli alkaa.
		// Aseta int-arvoksi ladattavan tason indeksi, tässä 1
		Application.LoadLevel(level);
	}

    public void ExitApp()
    {
        Application.Quit();
    }
}
