# Homebound
![screenshot](screenshot.png)
[Read More About Homebound64](https://portfolium.com/entry/homebound-64)

Homebound 64 is an homage to the never-released Earthbound 64 game by Nintendo. Contracted on Fiverr, development of this game began in Novemeber 2022. Since then, character models were created and I was tasked with animating the main character to be a mix of Banjo Kazooie and Link from The Legend of Zelda Ocarina of Time.

The main gameplay mechanic implemented for this game was the fishing mechanic. In the water areas of the game, fish swim in circles. When the player casts their rod, fish begin a series of checks that determine whether they will look at the blobber and if they will bite.

Textures and models were also provided by the client, however I created specialty cliff textures reminiscent of Nintendo 64 games like Earthbound and Ocarina of Time to better fit the look of the era.

I also designed the environments in this game based on descriptions provided by the client (ref. 14). The mountain area was inspired by Ocarina of Time's Death Mountain(ref. 13), and the ocean area was inspired by a screenshot from the cancelled Earthbound 64 game(ref. 7). Due to the large environment elements, low-res versions of the mountain and hills were placed in neighboring scenes to make the levels feel connected.

Scene transition colliders trigger scene change events, as well as finding a corresponding spawn point in the next scene. The versatility of this tool allows for easy drag and drop 'doors' that transition the player to the next scene. A level select tool was also created using Unity's dropdown list in conjuction with the SceneManagement library. Testers would select the name of the scene they wish to transition to.

A custom 'Fish UI' element was created to mimic the level transitions of the Nintendo 64 Donkey Kong Game.
