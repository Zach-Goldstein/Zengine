# Zengine
A light, test game engine built around MonoGame/XNA

@Floryan Things to look for

## ECS:
This is in the Engine subproject. Scenes contain entities and entities contain components. The Scene class and Entity class have managers for Entities and Components respectively

## DisplayObject:
The parody of this is in Engine/Graphics/Image.cs. This is a texture wrapper and handles loading & indexing single textures and spritesheets. Sprite.cs will is a component that can be registered with an Entity to display a still image, while AnimatedSprite.cs can support animations. Text.cs is also available to display text.

How spritesheets work: Texture is packed into some png file (e.g., FloryanHW/Content/spritesheet0.png). All the individual sprite locations are index with (x, y) and (h, w) (e.g., spritesheet.xml). When a spritesheet is loaded, the whole texture is loaded and individual sprites/frames are derived from the base texture. Animations are defined in another xml file (e.g., Animations.xml) and which helps index the set of frames for each animation in Engine/Graphics/AnimatedSprite.cs. Every update(), the frame swaps to the next in the list.

## Transform:
Each Entity has a Transform. You can find this in Engine/Util/Transform.cs. The variables are local, but calling global version traverses the parents and makes it moreso global. Unfortunately, like Zac's it's not contained within one matrix.

## Events:
Events are handled in the Event.cs class. The dispatcher is a single class, where events are registered and emitted. For a class to be a listener, it has to be an IEventListener and implement HandleEvent locally.

You can find an example of the events in FloryanHW/GameCode/Events/QuestManager.cs. The event is registered in Floryan/GameCode/Scenes/CollisionScene.cs and triggered in QuestManager.cs.

## Collision:
Collision is handled through Engine/Collision/Hitbox.cs. Note that matrix transformations are used to get the original hitbox points into the proper world coordinates. You can find the transformation functions in Engine/Util/MatrixTransforms.cs. An example of using collision information can be found in FloryanHW/GameCode/Entities/Player.cs.
