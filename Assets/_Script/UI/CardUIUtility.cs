using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUIUtility{
    public static readonly string SpriteRootPath = "Sprite/";
    public static readonly string DefaultSpritePath = "Sprite/default_back";

    public static Sprite DefaultSprite {
        get { return Resources.Load<Sprite>(DefaultSpritePath); }
    }

    public static Sprite LoadSprite(string path) {
        if (path == "")
            return DefaultSprite;

        var sp = Resources.Load<Sprite>(path);
        if (sp == null)
            return DefaultSprite;
        return sp;
    }

    public static Sprite LoadCardSprite(string spriteName) {
        if (spriteName == "")
            return DefaultSprite;
        return LoadSprite(SpriteRootPath + spriteName);
    }
}
