namespace RedBlueGames.IconPreviewer
{
    using System.IO;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    public class IconPreviewer : EditorWindow
    {
        private EditorIcon[] icons;

        private Vector2 scrollPosition;

        [MenuItem("Window/Icon Previewer")]
        private static void Init()
        {
            EditorWindow.GetWindow<IconPreviewer>(false, "Icon Previewer");
        }

        private void OnGUI()
        {
            if (this.icons == null)
            {
                this.LoadIconTextures();
            }

            EditorGUILayout.LabelField("Built in Icons", EditorStyles.boldLabel);
            this.scrollPosition = EditorGUILayout.BeginScrollView(this.scrollPosition, false, true);
            GUIContent[] content = new GUIContent[this.icons.Length];
            for (int i = 0; i < content.Length; ++i)
            {
                content[i] = icons[i].GUIContent;
            }
            GUILayout.SelectionGrid(0, content, 3);
            EditorGUILayout.EndScrollView();
        }

        private void LoadIconTextures()
        {
            var textFile = Resources.Load<TextAsset>("UnityEditorIcons");
            var paths = textFile.text.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);

            this.icons = new EditorIcon[paths.Length];
            for (int i = 0; i < paths.Length; ++i)
            {
                var path = paths[i];

                var icon = new EditorIcon();
                icon.Path = path;
                icon.Texture = EditorGUIUtility.IconContent(path).image;
                icon.GUIContent = new GUIContent(icon.Path, icon.Texture);

                this.icons[i] = icon;
            }
        }

        private class EditorIcon
        {
            public string Path { get; set; }

            public Texture Texture { get; set; }

            public GUIContent GUIContent { get; set; }
        }
    }
}