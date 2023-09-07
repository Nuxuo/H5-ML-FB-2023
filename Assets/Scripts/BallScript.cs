using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    SimpleMultiAgentGroup blue_agents_group = new SimpleMultiAgentGroup();
    SimpleMultiAgentGroup red_agents_group = new SimpleMultiAgentGroup();
    GameObject _last = null;
    GameObject Scorekeeper;
    public void Start()
    {
        var blue_agents = GameObject.FindGameObjectsWithTag("Blue team");
        var red_agents = GameObject.FindGameObjectsWithTag("Red team");


        foreach (GameObject item in blue_agents)
        {
            blue_agents_group.RegisterAgent(item.GetComponent<AgentScript>());
        }
        foreach (GameObject item in red_agents)
        {
            red_agents_group.RegisterAgent(item.GetComponent<AgentScript>());
        }

        Scorekeeper = GameObject.Find("Scorekeeper");
        Respawn(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Red Goal")
        {
            bool self_goal = false;
            Agent _last_agent = _last.GetComponent<AgentScript>();
            if(_last.CompareTag("Red team")){
                _last_agent.AddReward(-1f);
                self_goal = true;
            }
            else{
                _last_agent.AddReward(1f);
            }
            blue_agents_group.AddGroupReward(.5f);
            blue_agents_group.EndGroupEpisode();
            red_agents_group.AddGroupReward(-.5f);
            red_agents_group.EndGroupEpisode();

            if (Scorekeeper != null) { Scorekeeper.GetComponent<ScoreKepper>().ScoreUpdate(1,0,self_goal);  }
            Respawn();
        }
        else if (collision.collider.tag == "Blue Goal")
        {
            bool self_goal = false;
            Agent _last_agent = _last.GetComponent<AgentScript>();
            if(_last.CompareTag("Blue team")){
                _last_agent.AddReward(-1f);
                self_goal = true;
            }
            else{
                _last_agent.AddReward(1f);
            }
            blue_agents_group.AddGroupReward(-.5f);
            blue_agents_group.EndGroupEpisode();
            red_agents_group.AddGroupReward(.5f);
            red_agents_group.EndGroupEpisode();
            if (Scorekeeper != null) { Scorekeeper.GetComponent<ScoreKepper>().ScoreUpdate(0,1,self_goal);  }
            Respawn();
        }

        if(collision.collider.CompareTag("Red team") || collision.collider.CompareTag("Blue team")){
            _last = collision.gameObject;
        }


        if(this.transform.position.z > 0){
            red_agents_group.AddGroupReward(-.00025f);
        }
        else{
            blue_agents_group.AddGroupReward(-.00025f);
        }
    }

    public void Respawn()
    {
        transform.position = new Vector3(Random.Range(-5, 5),2f,Random.Range(-5, 5));
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }
}
