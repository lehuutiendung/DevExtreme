<template lang="">
    <div class="dx-field-value">
        <DxSelectBox
            :data-source="data"
            :value="items[0].Value"
            display-expr="StatusText"
            value-expr="Value"
            :showSelectionControls="true"
            @value-changed="valueChanged"
        />
    </div>
</template>
<script>
import DxSelectBox from 'devextreme-vue/select-box';
import ArrayStore from "devextreme/data/array_store";
export default {
    name: "SelectBox",
    components: {
        DxSelectBox,
    },
    data() {
        const items = this.$resourceVn.Status;
        return {
            items,
            data: new ArrayStore({
                data: items,
                key: 'Value'
            }),
        }
    },
    methods: {
    valueChanged(e)
    {
      console.log(e.value);
    }
  },
}
</script>
<style>
    @import "../../css/base/base-select-box.css";
</style>