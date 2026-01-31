# Card Boss Game - Unity Conversion

This is a Unity conversion of the Raylib card-based boss battle game.

## Setup Instructions

### 1. Create a New Unity Project
- Open Unity Hub
- Create a new 2D project
- Name it "CardBossGame"

### 2. Install Required Packages
1. Open Window > Package Manager
2. Install **TextMeshPro** (if not already installed)
3. Install **DOTween** (optional, for smooth animations)
   - Go to Window > Package Manager
   - Click the + button and select "Add package from git URL"
   - Enter: `https://github.com/Demigiant/dotween.git`
   - OR download from the Asset Store

### 3. Add Scripts
Copy all the .cs files to your Assets/Scripts folder:
- `Card.cs`
- `DeckManager.cs`
- `ComboChecker.cs`
- `CardUI.cs`
- `GameManager.cs`
- `MenuController.cs`

### 4. Create the UI Hierarchy

#### Main Canvas
1. Create a Canvas (right-click in Hierarchy > UI > Canvas)
2. Set Canvas Scaler to "Scale With Screen Size" (1920x1080 reference)

#### Menu Panel
1. Create Panel named "MenuPanel"
2. Add TextMeshPro text: "CARD BOSS GAME"
3. Add Button: "Start Game"

#### Game Panel
1. Create Panel named "GamePanel"
2. Add these UI elements:

**Boss Info:**
- TextMeshPro: "BossNameText" (top center)
- Slider: "BossHealthSlider"
- TextMeshPro: "BossHealthText"

**Player Info:**
- TextMeshPro: "PlayerHealthText" (top left)
- TextMeshPro: "DrawPileText"
- TextMeshPro: "DiscardPileText"
- TextMeshPro: "TurnText"

**Combat:**
- TextMeshPro: "ComboMessageText" (center, large font)
- TextMeshPro: "DamagePreviewText"
- Button: "AttackButton" with text "ATTACK"

**Hand Container:**
- Create Empty GameObject named "HandContainer"
- Add Horizontal Layout Group component
- Set spacing to 10-20
- Set Child Force Expand to both Width and Height

#### Win/Lost Panels
1. Create Panel named "WinPanel"
   - Add text: "YOU WIN!"
   - Add restart button
2. Create Panel named "LostPanel"
   - Add text: "YOU LOST!"
   - Add restart button

### 5. Create Card Prefab

1. Create Empty GameObject named "Card"
2. Add these components:
   - Image component (background)
   - Button component
   - Add child Image named "SelectedHighlight" (make it bright blue/yellow)
   - Add child TextMeshPro named "RankText" (top-left)
   - Add child TextMeshPro named "SuitText" (bottom-right)
3. Add CardUI script component
4. Set Layout Element (recommended size: 150x200)
5. Drag to Prefabs folder

### 6. Setup GameManager

1. Create Empty GameObject named "GameManager"
2. Add GameManager script
3. Assign all UI references in inspector:
   - Drag MenuPanel, GamePanel, WinPanel, LostPanel
   - Assign all text fields, sliders, buttons
   - Assign Card Prefab
   - Assign HandContainer transform
   - Set boss HP values (100, 200)
   - Set player HP values

### 7. Setup Menu Controller

1. Add MenuController script to MenuPanel
2. Assign start/restart/quit buttons

### 8. Configure Initial State

1. Make sure only MenuPanel is active initially
2. Disable GamePanel, WinPanel, LostPanel

## Game Features

### Combos
- **Triple Aces**: 3x damage multiplier
- **Triple Kings/Queens**: 2.5x damage multiplier
- **Triple any other rank**: 2x damage multiplier
- **Pair of Aces**: 1.5x damage multiplier
- **Any pair**: 1.3x damage multiplier

### Game Flow
1. Start at menu
2. Level 1: Boss with 100 HP
3. Level 2: Boss with 200 HP, player gets 1000 HP
4. Win or lose screens

### Controls
- Click cards to select them
- Click "ATTACK" button to attack with selected cards
- Cards are automatically refilled from the deck

## Customization

You can adjust these values in the GameManager inspector:
- Boss 1 Max HP
- Boss 2 Max HP
- Player Start HP
- Level 2 Player HP
- Boss attack damage range (edit in code: 5-12)

## Optional Enhancements

### Animations (if using DOTween)
The CardUI script includes animation support. Cards will smoothly move when dealt.

### Sound Effects
Add AudioSource components to play sounds for:
- Card selection
- Attacking
- Combos
- Boss attacks

### Visual Polish
- Add particle effects for combos
- Add screen shake on attacks
- Animate health bars
- Add card flip animations

## Troubleshooting

**Cards not appearing:**
- Check that Card Prefab is assigned in GameManager
- Verify HandContainer has Horizontal Layout Group
- Make sure CardUI script references are set

**UI not updating:**
- Verify all TextMeshPro fields are assigned in GameManager
- Check that panels are properly enabled/disabled

**DOTween errors:**
If you don't want to use DOTween, remove the line:
```csharp
transform.DOMove(transform.position, duration).SetEase(Ease.OutQuad);
```
from CardUI.cs and replace with:
```csharp
// Simple instant positioning
transform.position = transform.position;
```

## Build Settings

To build the game:
1. File > Build Settings
2. Add current scene
3. Select platform (PC, Mac, WebGL)
4. Click Build
