<template>
    <div class="box-combobox-text">
        <div class="wrap-combobox-text combobox-room" @click="showDataCombobox()">
            <input type="text" class="combobox" :value="itemComboboxName"/>
            <div class="icon-34 combobox-btn">
                <div class="icon-20 icon-combobox"></div>
            </div>
        </div>
        <ul class="combobox-data" :class="{'combobox-data-flex' : showData}">
            <!-- Binding data here! -->
            <li class="combobox-data-item" :class="{'item-active' : current == index ? true:false}" v-for="(item, index) in dataCombobox" :key="index" @click="getValueCombobox(item.Name, index)">
                <span>{{ item.Name }}</span>
            </li>
        </ul>
  </div>
</template>
<script>
export default {
    name: "ComboboxText",
    props:{
        dataCombobox:{
            type: Array,
            default(){
                return [];
            }
        }
    },
    data() {
        return {
            showData: false,         //Mặc định data của combobox bị ẩn
            itemComboboxName: "",    //Giá trị của input
            current: 0,              //Item được click
        }
    },
    created() {
        this.itemComboboxName = this.dataCombobox[0].Name;
    },
    methods: {
        /**
         * @description Ẩn/Hiện danh sách data của combobox
         * @date 19/9/2021
         * @createdBy LHTDung
         */
        showDataCombobox(){
            this.showData = !this.showData;
        },

        /**
         * @description Xử lý sự kiện click chọn item data
         * @date 19/9/2021
         * @createdBy LHTDung
         */
        getValueCombobox(item, index){
            this.itemComboboxName = item;
            this.current = index;
            this.showData = !this.showData;
        }
    },
}
</script>
<style scoped>
    @import "../../../css/base/combobox-text/base-combobox-text.css";
</style>