# Unity ML-Agents Goal Scoring Environment

This project demonstrates a Unity ML-Agents environment where agents interact with goals and score points. The environment includes several key scripts to control agent behavior, manage goals, and keep track of scores.

## Scripts

### `AgentScript`

- The `AgentScript` controls the behavior of individual agents in the environment.
- It inherits from the `Agent` class provided by Unity ML-Agents.
- Key features include:
  - Collecting observations from the environment.
  - Moving the agent based on continuous action inputs.
  - Rewarding the agent based on its distance from a goal.
  - Handling collisions with various objects.
  
### `GoalScript`

- The `GoalScript` manages the goals in the environment and handles scoring logic.
- It defines two agent groups (blue and red) and tracks the last agent interacting with a goal.
- Key features include:
  - Registering agents with their respective groups.
  - Updating rewards and scores based on goal interactions.
  - Respawning the goals to new positions.
  
### `ScoreKeeper`

- The `ScoreKeeper` script keeps track of scores for the blue and red teams.
- It also counts self-goals and displays the scores in a UI text element.
- Key features include:
  - Initializing and updating scores.
  - Displaying scores with the number of self-goals.
  
## Getting Started

1. Clone this repository to your local machine.
2. Open the project in Unity.
3. Run the scene to observe agent behavior and goal interactions.

## Usage

- Use this project as a starting point for creating custom Unity ML-Agents environments.
- Experiment with different agent behaviors and rewards.
- Extend the project by adding more agents, goals, or complex interactions.
  
## Acknowledgments

- This project was created with Unity ML-Agents, a toolkit provided by Unity Technologies for developing machine learning agents in Unity environments.
