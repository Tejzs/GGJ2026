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
   - Unity will prompt you to import TMP Essentials on first use

### 3. Install DOTween (Optional but Recommended)
DOTween provides smooth, professional animations for the cards.

**Option A: Unity Asset Store (Recommended)**
1. Open the [DOTween Asset Store page](https://assetstore.unity.com/packages/tools/animation/dotween-hotween-v2-27676)
2. Click "Add to My Assets" (it's free)
3. In Unity, open Window > Package Manager
4. Switch to "My Assets" at the top
5. Find "DOTween (HOTween v2)"
6. Click Download, then Import
7. Import all files when prompted

**Option B: Download from Website**
1. Go to [DOTween Downloads](https://dotween.demigiant.com/download.php)
2. Download "DOTween_vX.X.XXX.unitypackage"
3. In Unity, go to Assets > Import Package > Custom Package
4. Select the downloaded .unitypackage file
5. Import all files

**Setup DOTween (REQUIRED):**

After importing, you need to run the setup:

1. **If the DOTween Setup window appears automatically:**
   - Click "Setup DOTween"
   - Click "Apply"
   - You're done!

2. **If the setup window doesn't appear (or you closed it):**
   - Go to **Tools > Demigiant > DOTween Utility Panel**
   - In the panel that opens, click the **"Setup DOTween..."** button
   - Click **"Apply"** in the next window
   - Optional: Click **"Create ASMDEF"** for faster compile times
   - Close the panel

3. **Verify DOTween is set up:**
   - You should see a "DOTweenSettings" file in your Resources folder
   - No errors in the Console
   - Ready to use!

**If you skip DOTween:**
Use the CardUI_Simple.cs script instead, which provides the same functionality using Unity's built-in Coroutines.

### 4. Add Scripts
Copy all the .cs files to your Assets/Scripts folder:
- `Card.cs`
- `DeckManager.cs`
- `ComboChecker.cs`
- `CardUI.cs` - **Use this if you installed DOTween**
- `CardUI_Simple.cs` - **Use this if you skipped DOTween** (delete CardUI.cs if using this)
- `GameManager.cs`
- `MenuController.cs`

**Important:** Only use ONE CardUI script - either CardUI.cs (with DOTween) OR CardUI_Simple.cs (without DOTween).

### 5. Create the UI Hierarchy

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

### 6. Create Card Prefab

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

### 7. Setup GameManager

1. Create Empty GameObject named "GameManager"
2. Add GameManager script
3. Assign all UI references in inspector:
   - Drag MenuPanel, GamePanel, WinPanel, LostPanel
   - Assign all text fields, sliders, buttons
   - Assign Card Prefab
   - Assign HandContainer transform
   - Set boss HP values (100, 200)
   - Set player HP values

### 8. Setup Menu Controller

1. Add MenuController script to MenuPanel
2. Assign start/restart/quit buttons

### 9. Configure Initial State

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

### Animations
The CardUI script includes smooth animation support using Unity's built-in Coroutines. Cards will smoothly move when dealt from the draw pile to your hand.

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

**Which CardUI script to use:**
- **With DOTween installed**: Use `CardUI.cs` for smooth animations
- **Without DOTween**: Use `CardUI_Simple.cs` (same functionality, uses Coroutines)
- Both scripts are functionally identical, just different animation methods

**DOTween not set up properly:**
- If you see errors like `The type or namespace name 'DG' could not be found`
- **Solution:** Go to **Tools > Demigiant > DOTween Utility Panel**
- Click **"Setup DOTween..."** button
- Click **"Apply"**
- Wait for Unity to recompile
- If the menu item doesn't exist, DOTween wasn't imported correctly - try reimporting

**DOTween menu (Tools > Demigiant) doesn't exist:**
- DOTween wasn't imported properly
- Go to Window > Package Manager > My Assets
- Find DOTween and click "Remove"
- Then re-import it following the installation steps above
- Make sure to import ALL files when prompted

**Still having DOTween issues:**
- Just use `CardUI_Simple.cs` instead - delete `CardUI.cs` from your project
- Update the Card Prefab to use CardUI_Simple component instead
- Works exactly the same, just uses Unity's native animation system

## Build Settings

To build the game:
1. File > Build Settings
2. Add current scene
3. Select platform (PC, Mac, WebGL)
4. Click Build
