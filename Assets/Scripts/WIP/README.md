### Goal of the project:
Right now, I just want each player to be able to click on any circle on the screen, regardless of owner, causing that circle to change color and spawn in a duplicate (with the new color).

<hr>

### Rundown:
I have a prefab called Circle(1) which has a ColorCircle component attached to it. When the player left clicks on the circle, the component's PerformAction() method is invoked. This tells the circle to change color and spawn in a duplicate.

I like to keep the client-run code separate from the server-run code, so I have two behaviour classes, one being the ClientCircleBehaviour and the other being ServerHostCircleBehaviour. Basically if the circle is not being run on the server/host, it uses the ClientCircleBehaviour which uses [ServerRPC] to tell the server to run some code, ultimately making its way back to that client with the updated changes.

<hr>

### The problems I'm having:
Spawning in duplicate circles works fine on both the client and server, but the colors only change for those duplicates on the server. All of the duplicates circles appear black (prefab's default color) until I click on one. Then the behaviour kicks in as expected, but the duplicate THAT circle spawns is also black.

<img src="https://user-images.githubusercontent.com/42879674/180105770-60f2fa96-4fa0-4c83-a348-6368ff31d7f6.png" width="500">
The above image shows how the client can spawn in duplicates, but only the circle they click on will change colors.

On the server specifically:
- ChangeColor() and SpawnDuplicate() are not being called in the correct order. Even though I'm calling ChangeColor() first, SpawnDuplicate() always winds up executing first. This causes the duplicate circle to use the parent circle's ORIGINAL color, not the new color.
<img src="https://user-images.githubusercontent.com/42879674/180105330-84b080b8-c870-4b8d-913f-5756f8027224.png" width="500">
In the above image, the black circle should be green. Instead, it is the green circle's ORIGINAL color: black.
