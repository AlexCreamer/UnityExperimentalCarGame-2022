using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public string PlayerName;
    public int ConnectionID;
    public ulong PlayerSteamID;
    public bool AvatarReceived;

    public Text PlayerNameText;
    public RawImage PlayerIcon;
    public Text PlayerReadyText;
    public bool Ready;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    public void ChangeReadyStatus()
    {
        if (Ready) // Ready 
        {
            PlayerReadyText.text = "Ready";
            PlayerReadyText.color = Color.green;        
        }
        else // Not ready
        {
            PlayerReadyText.text = "Unready";
            PlayerReadyText.color = Color.red;
        }
    }

    private void Start()
    {
        ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }

    void GetPlayerIcon()
    {
        int ImageID = SteamFriends.GetLargeFriendAvatar((CSteamID)PlayerSteamID);
        if (ImageID == -1) { return; }
        PlayerIcon.texture = GetSteamImageAsTexture(ImageID);
    }

    private Texture2D GetSteamImageAsTexture(int iImage)
    {
        Texture2D texture = null;

        bool isValid = SteamUtils.GetImageSize(iImage, out uint width, out uint height);
        if (isValid)
        {
            byte[] image = new byte[width * height * 4];

            isValid = SteamUtils.GetImageRGBA(iImage, image, (int)(width * height * 4));

            if (isValid)
            {
                texture = new Texture2D((int)width, (int)height, TextureFormat.RGBA32, false, true);
                texture.LoadRawTextureData(image);
                texture.Apply();
            }
        }
        AvatarReceived = true;
        return texture;
    }

    public void SetPlayerValues()
    {
        PlayerNameText.text = PlayerName;
        ChangeReadyStatus();
        if (!AvatarReceived) { GetPlayerIcon(); }
    }

    private void OnImageLoaded(AvatarImageLoaded_t callback)
    {
        if (callback.m_steamID.m_SteamID == PlayerSteamID)
        {
            PlayerIcon.texture = GetSteamImageAsTexture(callback.m_iImage);
        }
        else
        {
            return;
        }
    }
}
