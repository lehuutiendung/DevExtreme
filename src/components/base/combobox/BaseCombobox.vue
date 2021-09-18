<template>
<div class="wrap-combobox">
    <div class="dx-field">
        <div class="dx-field-value">
            <DxTagBox
            :items="simpleProducts"
            :show-selection-controls="true"
            :search-enabled="true"
            :placeholder="placeholder"
            />
        </div>
        <div class="icon-36 wrap-icon-combobox">
            <div class="icon-20 icon-combobox">

            </div>
        </div>
    </div>
</div>
    
</template>

<script>
import DxTagBox from 'devextreme-vue/tag-box';
export default {
    components: {
        DxTagBox,
    },
    props:{
        placeholder:{
            type: String,
            default(){
                return "";
            }
        }
    },
    data(){
        return {
            simpleProducts: [
                'HD Video Player',
                'SuperHD Video Player',
                'SuperPlasma 50',
                'SuperLED 50',
                'SuperLED 42',
                'SuperLCD 55',
                'SuperLCD 42',
                'SuperPlasma 65',
                'SuperLCD 70',
                'Projector Plus',
                'Projector PlusHT',
                'ExcelRemote IR',
                'ExcelRemote Bluetooth',
                'ExcelRemote IP'
            ],
        }
    }
}
</script>
<style scoped>
    @import "../../../css/base/combobox/base-combobox.css";
</style>