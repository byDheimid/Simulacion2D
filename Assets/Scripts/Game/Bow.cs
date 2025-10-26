using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public Parable m_parable;
    public Flotability m_flotability;
    public GameObject m_prefab;

    private BowArrow m_bowArrow;

    [HideInInspector] public List<BowArrow> m_arrows = new();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) Initialize();
        if (Input.GetKey(KeyCode.Mouse0)) m_bowArrow.Aim();
        if (Input.GetKeyUp(KeyCode.Mouse0)) m_bowArrow.Shoot();
    }

    void Initialize()
    {
        Instantiate(m_prefab, transform).TryGetComponent(out m_bowArrow);

        m_arrows.Where(c => c.gameObject != null).ToList().ForEach(c => Destroy(c.gameObject));
        m_arrows.Clear();

        m_arrows.Add(m_bowArrow);

        //-> PARABLE
        m_bowArrow.m_parable = m_parable;
        m_bowArrow.m_flotability = m_flotability;

        m_bowArrow.m_initialPosition = transform.position;
        m_bowArrow.transform.position = transform.position;
    }
}
