<template>
    <div class="wrap-custome-column" :style="{display : showSetting}">
        <div class="wrap-custome-column-content">
            <div class="custome-row-flex custome-title">
                <div class="title">Tùy chỉnh cột</div>
                <div class="icon-36 button-refresh">
                    <div class="icon-20 icon-refresh"></div>
                </div>
            </div>
            <InputSearch/>
            <draggable class="draggable" v-model="listHeader">
                <transition-group>
                    <div class="wrap-item-column" v-for="(item, index) in listHeader" :key="index">
                        <div class="wrap-input-checkbox">
                            <div class="input-checkbox-default">
                            </div>
                            <div class="title-item">{{ item.Caption }}</div>
                        </div>
                        <div class="icon-24 wrap-button-move">
                            <div class="icon-20 icon-button-move"></div>
                        </div>
                    </div>
                </transition-group>
            </draggable>
        </div>
        <div class="draggable-wrap-button">
            <Button class="button-green" :buttonName="this.$resourceVn.ButtonSaveText"/>
        </div>
    </div>
</template>
<script>
import fakeData from "../../js/fake-data/data-grid-main"

import draggable from 'vuedraggable'
import InputSearch from '../../components/base/input-search/BaseInputSearch.vue'
import Button from "../base/button/BaseButton.vue"
export default {
    name: "CustomeColumn",
    
    components: {
            draggable,
            InputSearch,
            Button,
    },
    props:{
        showPopupEditColumn:{
            type: Boolean,
            default(){
                return false;
            }
        }
    },
    data() {
        return {
            listHeader: fakeData.listHeader,
        }
    },

    computed: {
        /**
         * @description Thay đổi trạng thái của popup setting "Tùy chỉnh cột"
         * @date 18/09/2021
         * @createdBy LHTDung
         */
        showSetting() {
            if (this.showPopupEditColumn) {
                return "flex";
            } else {
                return "none";
            }
        },
    },
    methods: {
        
    },
}
</script>
<style>
    @import "../../css/layout/custome-column.css";
</style>