﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UICombatEnd : UIPanel
{
    public class CombatEndParam
    {
        public string roleIdLeft;
        public string roleIdRight;

        public string pet1RoleIdLeft;
        public string pet1RoleIdRight;

        public string pet2RoleIdLeft;
        public string pet2RoleIdRight;

        public bool weWin;
        public int myRankVal;
        public int myOldRankVal;
        public int myScoreVal;
        public int myOldScoreVal;
        public List<KeyValuePair<int, int>> items = new List<KeyValuePair<int, int>>();
        public int rewardId;
        public List<DamageDataParamItem> leftDmgParams = new List<DamageDataParamItem>();
        public List<DamageDataParamItem> rightDmgParams = new List<DamageDataParamItem>();
    }

    public StateHandle m_winLose;
    public StateHandle m_btnDamage;
    public UI3DView2 m_role3DView;
    public ImageEx m_pet1HeadLeft;
    public ImageEx m_pet1HeadRight;
    public ImageEx m_pet2HeadLeft;
    public ImageEx m_pet2HeadRight;
    public TextEx m_myRankVal;
    public StateHandle m_rankIncDec;
    public TextEx m_myRankDelta;
    public TextEx m_myScoreDelta;
    public StateGroup m_itemGroup;

    private CombatEndParam m_curParam;

    public override void OnInitPanel()
    {
        m_btnDamage.AddClick(() =>
        {
            var dmgParam = new UIDamageData.DamageDataParam();
            dmgParam.leftParams = m_curParam.leftDmgParams;
            dmgParam.rightParams = m_curParam.rightDmgParams;
            UIMgr.instance.Open<UIDamageData>(dmgParam);
        });
    }

    //显示,保证在初始化之后
    public override void OnOpenPanel(object param)
    {
        m_curParam = (CombatEndParam)param;

        m_winLose.SetState(m_curParam.weWin ? 0 : 1);
        RoleCfg roleCfgLeft = RoleCfg.Get(m_curParam.roleIdLeft);
        if (roleCfgLeft != null)
        {
            m_role3DView.SetLeftModel(roleCfgLeft.mod, roleCfgLeft.uiModScale, AniFxMgr.Ani_DaiJi);
        }
        else
        {
            m_role3DView.SetLeftModel(null);
        }
        RoleCfg roleCfgRight = RoleCfg.Get(m_curParam.roleIdRight);
        if (roleCfgRight != null)
        {
            m_role3DView.SetRightModel(roleCfgRight.mod, roleCfgRight.uiModScale, AniFxMgr.Ani_DaiJi);
        }
        else
        {
            m_role3DView.SetRightModel(null);
        }

        RoleCfg pet1LeftCfg = m_curParam.pet1RoleIdLeft == "" ? null : RoleCfg.Get(m_curParam.pet1RoleIdLeft);
        if (pet1LeftCfg != null)
        {
            m_pet1HeadLeft.gameObject.SetActive(true);
            m_pet1HeadLeft.Set(RoleCfg.GetHeadIcon(m_curParam.pet1RoleIdLeft));
        }
        else
        {
            m_pet1HeadLeft.gameObject.SetActive(false);
        }
        RoleCfg pet1RightCfg = m_curParam.pet1RoleIdRight == "" ? null : RoleCfg.Get(m_curParam.pet1RoleIdRight);
        if (pet1RightCfg != null)
        {
            m_pet1HeadRight.gameObject.SetActive(true);
            m_pet1HeadRight.Set(RoleCfg.GetHeadIcon(m_curParam.pet1RoleIdRight));
        }
        else
        {
            m_pet1HeadRight.gameObject.SetActive(false);
        }

        RoleCfg pet2LeftCfg = m_curParam.pet2RoleIdLeft == "" ? null : RoleCfg.Get(m_curParam.pet2RoleIdLeft);
        if (pet2LeftCfg != null)
        {
            m_pet2HeadLeft.gameObject.SetActive(true);
            m_pet2HeadLeft.Set(RoleCfg.GetHeadIcon(m_curParam.pet2RoleIdLeft));
        }
        else
        {
            m_pet2HeadLeft.gameObject.SetActive(false);
        }
        RoleCfg pet2RightCfg = m_curParam.pet2RoleIdRight == "" ? null : RoleCfg.Get(m_curParam.pet2RoleIdRight);
        if (pet2RightCfg != null)
        {
            m_pet2HeadRight.gameObject.SetActive(true);
            m_pet2HeadRight.Set(RoleCfg.GetHeadIcon(m_curParam.pet2RoleIdRight));
        }
        else
        {
            m_pet2HeadRight.gameObject.SetActive(false);
        }

        m_myRankVal.text = (m_curParam.myRankVal + 1).ToString();
        int rankValDelta = m_curParam.myOldRankVal - m_curParam.myRankVal;  //这里用old减new，因为名次越好，数字越小
        m_rankIncDec.SetState(rankValDelta == 0 ? 2 : (rankValDelta > 0 ? 0 : 1));
        m_myRankDelta.text = rankValDelta.ToString();
        int scoreValDelta = m_curParam.myScoreVal - m_curParam.myOldScoreVal;
        m_myScoreDelta.text = scoreValDelta.ToString();


        List<RewardItem> itemsList = RewardCfg.GetRewardsDefinite(m_curParam.rewardId);
        m_itemGroup.SetCount(itemsList.Count);
        for (var i = 0; i < itemsList.Count; ++i)
        {
            var uiItem = m_itemGroup.Get<UIItemIcon>(i);
            var dataItem = itemsList[i];
            uiItem.Init(dataItem.itemId, dataItem.itemNum);
        }
    }

    //关闭，保证在初始化之后
    public override void OnClosePanel()
    {
        LevelMgr.instance.GotoMaincity();
    }

    //更新，保证在初始化之后
    public override void OnUpdatePanel()
    {

    }
}